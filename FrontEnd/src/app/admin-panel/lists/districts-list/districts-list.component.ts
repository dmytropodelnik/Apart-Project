import { Component, OnInit } from '@angular/core';
import { District } from 'src/app/models/Location/district.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';
import ImageHelper from '../../../utils/imageHelper';
import { AdminContentService } from 'src/app/services/admin-content.service';

@Component({
  selector: 'app-districts-list',
  templateUrl: './districts-list.component.html',
  styleUrls: ['./districts-list.component.css']
})
export class DistrictsListComponent implements OnInit {

  districts: District[] | null = null;
  district: District | null = null;
  searchDistrict: string = '';
  checkedDistrict: number | null = null;
  imageHelper: any = ImageHelper;

  page: number = 1;
  pageSize: number = 15;

  constructor(
    private adminContentService: AdminContentService
  ) {

  }

  search(): void {
    fetch('https://apartmain.azurewebsites.net/api/districts/search?district=' + this.searchDistrict, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.districts = data.districts;
        } else {
          alert('Search error!');
        }
        this.searchDistrict = '';
      })
      .catch((ex) => {
        alert(ex);
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
          alert('Adding error!');
        }
        this.district = null;
      })
      .catch((ex) => {
        alert(ex);
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
          alert('Editing error!');
        }
        this.district = null;
      })
      .catch((ex) => {
        alert(ex);
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
          alert('Editing error!');
        }
        this.district = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getDistricts(): void {
    fetch(`https://apartmain.azurewebsites.net/api/districts/getdistricts?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.districts = data.districts;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  collectElements(districts: District[]): void {
    for (let item of districts) {
      this.districts?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(`https://apartmain.azurewebsites.net/api/districts/getdistricts?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.districts);
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
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
