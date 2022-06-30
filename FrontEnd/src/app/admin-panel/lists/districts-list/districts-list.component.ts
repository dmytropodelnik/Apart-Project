import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { District } from 'src/app/models/Location/district.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';
import ImageHelper from '../../../utils/imageHelper';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-districts-list',
  templateUrl: './districts-list.component.html',
  styleUrls: ['./districts-list.component.css'],
})
export class DistrictsListComponent implements OnInit {
  districts: District[] | null = null;
  district: District | null = null;
  searchDistrict: string = '';
  checkedDistrict: number | null = null;
  imageHelper: any = ImageHelper;

  page: number = 1;
  pageSize: number = 15;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private adminContentService: AdminContentService,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  search(): void {
    fetch(
      'https://localhost:44381/api/districts/search?district=' +
        this.searchDistrict,
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
          this.districts = data.districts;
        } else {
          this.showAlert('Search error!');
        }
        this.searchDistrict = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  addDistrict(): void {
    let district = {
      name: this.district,
    };

    fetch('https://apartmain.azurewebsites.net/api/districts/adddistrict', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(district),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getDistricts();
        } else {
          this.showAlert('Adding error!');
        }
        this.district = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  editDistrict(): void {
    let district = {
      id: this.checkedDistrict,
      name: this.district,
    };

    fetch('https://apartmain.azurewebsites.net/api/districts/editdistrict', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(district),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getDistricts();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.district = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  deleteDistrict(): void {
    let district = {
      id: this.checkedDistrict,
      name: this.district,
    };

    fetch('https://apartmain.azurewebsites.net/api/districts/deletedistrict', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(district),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getDistricts();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.district = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  getDistricts(): void {
    fetch(
      `https://localhost:44381/api/districts/getdistricts?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.districts = data.districts;
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  collectElements(districts: District[]): void {
    for (let item of districts) {
      this.districts?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(
      `https://localhost:44381/api/districts/getdistricts?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.collectElements(data.districts);
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  setDistrict(district: District): void {
    this.checkedDistrict = district.id;
    this.district = district;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getDistricts();
  }
}
