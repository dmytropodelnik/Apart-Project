import { Component, OnInit } from '@angular/core';
import { District } from 'src/app/models/Location/district.item';

import AuthHelper from '../../../utils/authHelper';

@Component({
  selector: 'app-districts-list',
  templateUrl: './districts-list.component.html',
  styleUrls: ['./districts-list.component.css']
})
export class DistrictsListComponent implements OnInit {

  districts: District[] | null = null;
  district: string | null = null;
  checkedDistrict: number | null = null;

  constructor() {}

  addDistrict(): void {
    let district = {
      name: this.district,
    };

    fetch('https://localhost:44381/api/districts/adddistrict', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
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
        this.district = '';
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

    fetch('https://localhost:44381/api/districts/editdistrict', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(district),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getDistricts();
        } else {
          alert('Editing error!');
        }
        this.district = '';
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

    fetch('https://localhost:44381/api/districts/deletedistrict', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(district),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getDistricts();
        } else {
          alert('Editing error!');
        }
        this.district = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getDistricts(): void {
    fetch('https://localhost:44381/api/districts/getdistricts', {
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

  setDistrict(id: number | null, district: string): void {
    this.checkedDistrict = id;
    this.district = district;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getDistricts();
  }

}
