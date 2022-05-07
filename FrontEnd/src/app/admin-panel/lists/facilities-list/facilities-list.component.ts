import { Component, OnInit } from '@angular/core';
import { Facility } from 'src/app/models/facility.item';
import { FacilityType } from 'src/app/models/facilitytype.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';
import ImageHelper from '../../../utils/imageHelper';
import { AdminContentService } from 'src/app/services/admin-content.service';


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

  constructor(
    private adminContentService: AdminContentService
  ) {
    this.facility = new Facility();
  }

  search(): void {
    fetch('https://apartmain.azurewebsites.net/api/facilities/search?facility=' + this.searchFacility, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.facilities = data.facilities;
        } else {
          alert('Search error!');
        }
        this.searchFacility = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addFacility(): void {
    let facility = {
      text: this.facility.text,
      facilityTypeId: this.facility.facilityType.id,
      imageId: this.facility.image?.id,
    };

    fetch('https://apartmain.azurewebsites.net/api/facilities/addfacility', {
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
          alert('Adding error!');
        }
        this.resetFacility();
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editFacility(): void {
    let facility = {
      id: this.checkedFacility,
      text: this.facility.text,
      facilityType: this.facility.facilityType?.id,
      suggestion: null,
    };

    fetch('https://apartmain.azurewebsites.net/api/facilities/editfacility', {
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
          alert('Editing error!');
        }
        this.resetFacility();
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteFacility(): void {
    let facility = {
      id: this.checkedFacility,
      text: this.facility.text,
      facilityTypeId: this.facility.facilityType.id,
      imageId: this.facility.image?.id,
    };

    fetch('https://apartmain.azurewebsites.net/api/facilities/deletefacility', {
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
          alert('Deleting error!');
        }
        this.resetFacility();
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  resetFacility(): void {
    this.facility.text = '';
    this.facility.image = null;
    this.facility.facilityType = new FacilityType();
    this.facility.suggestion = null;
  }

  getFacilities(): void {
    fetch(`https://apartmain.azurewebsites.net/api/facilities/getfacilities?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.facilities = data.facilities;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  collectElements(facilities: Facility[]): void {
    for (let item of facilities) {
      this.facilities?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(`https://apartmain.azurewebsites.net/api/facilities/getfacilities?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.facilities);
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
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
    fetch('https://apartmain.azurewebsites.net/api/facilitytypes/gettypes', {
      method: 'GET',
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
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  ngOnInit(): void {
    this.getFacilities();
    this.getFacilityTypes();
  }
}
