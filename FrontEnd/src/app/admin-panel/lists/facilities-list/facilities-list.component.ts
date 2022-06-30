import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Facility } from 'src/app/models/facility.item';
import { FacilityType } from 'src/app/models/facilitytype.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';
import ImageHelper from '../../../utils/imageHelper';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-facilities-list',
  templateUrl: './facilities-list.component.html',
  styleUrls: ['./facilities-list.component.css'],
})
export class FacilitiesListComponent implements OnInit {
  facilities: Facility[] | null = null;
  facility: Facility;
  searchFacility: string = '';
  checkedFacility: number | null = null;
  imageHelper: any = ImageHelper;

  page: number = 1;
  pageSize: number = 15;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private adminContentService: AdminContentService,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {
    this.facility = new Facility();
  }

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  search(): void {
    fetch(
      'https://localhost:44381/api/facilities/search?facility=' +
        this.searchFacility,
      {
        method: 'GET',
        headers: {
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.facilities = data.facilities;
        } else {
          this.showAlert('Search error!');
        }
        this.searchFacility = '';
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  addFacility(): void {
    let facility = {
      text: this.facility.text,
      facilityTypeId: this.facility.facilityType.id,
      imageId: this.facility.image?.id,
    };

    fetch('https://localhost:44381/api/facilities/addfacility', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(facility),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getFacilities();
        } else {
          this.showAlert('Adding error!');
        }
        this.resetFacility();
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  editFacility(): void {
    let facility = {
      id: this.checkedFacility,
      text: this.facility.text,
      facilityType: this.facility.facilityType?.id,
      suggestion: null,
    };

    fetch('https://localhost:44381/api/facilities/editfacility', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(facility),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getFacilities();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.resetFacility();
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  deleteFacility(): void {
    let facility = {
      id: this.checkedFacility,
      text: this.facility.text,
      facilityTypeId: this.facility.facilityType.id,
      imageId: this.facility.image?.id,
    };

    fetch('https://localhost:44381/api/facilities/deletefacility', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(facility),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getFacilities();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Deleting error!');
        }
        this.resetFacility();
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  resetFacility(): void {
    this.facility.text = '';
    this.facility.image = null;
    this.facility.facilityType = new FacilityType();
    this.facility.suggestion = null;
  }

  getFacilities(): void {
    fetch(
      `https://localhost:44381/api/facilities/getfacilities?page=${this.page}&pageSize=${this.pageSize}`,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.facilities = data.facilities;
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  collectElements(facilities: Facility[]): void {
    for (let item of facilities) {
      this.facilities?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(
      `https://localhost:44381/api/facilities/getfacilities?page=${this.page}&pageSize=${this.pageSize}`,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.facilities);
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  setFacility(facility: Facility): void {
    this.checkedFacility = facility.id;
    this.facility.text = facility.text;
    this.facility.image = facility.image;
    this.facility.suggestion = facility.suggestion;
    this.facility.facilityType.id = facility.facilityType.id;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  getFacilityTypes(): void {
    fetch('https://localhost:44381/api/facilitytypes/gettypes', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          let typesAdd = document.getElementById('addFacilityTypes');
          let newOption;
          let counter = 1;
          for (let type of data.types) {
            newOption = new Option(type.type, counter.toString());
            typesAdd?.append(newOption);
            counter++;
          }

          let typesEdit = document.getElementById('editFacilityTypes');
          counter = 1;
          for (let type of data.types) {
            newOption = new Option(type.type, counter.toString());
            typesEdit?.append(newOption);
            counter++;
          }
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  ngOnInit(): void {
    this.getFacilities();
    this.getFacilityTypes();
  }
}
