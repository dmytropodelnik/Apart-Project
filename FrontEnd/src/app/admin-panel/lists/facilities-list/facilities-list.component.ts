import { Component, OnInit } from '@angular/core';
import { Facility } from 'src/app/models/facility.item';

import AuthHelper from '../../../utils/authHelper';

@Component({
  selector: 'app-facilities-list',
  templateUrl: './facilities-list.component.html',
  styleUrls: ['./facilities-list.component.css']
})
export class FacilitiesListComponent implements OnInit {

  facilities: Facility[] | null = null;
  facility: string | null = null;
  checkedFacility: number | null = null;

  constructor() {}

  addFacility(): void {
    let facility = {
      name: this.facility,
    };

    fetch('https://localhost:44381/api/facilities/addfacility', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
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
        this.facility = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editFacility(): void {
    let facility = {
      id: this.checkedFacility,
      name: this.facility,
    };

    fetch('https://localhost:44381/api/facilities/editfacility', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(facility),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getFacilities();
        } else {
          alert('Editing error!');
        }
        this.facility = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteFacility(): void {
    let facility = {
      id: this.checkedFacility,
      name: this.facility,
    };

    fetch('https://localhost:44381/api/facilities/deletefacility', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(facility),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getFacilities();
        } else {
          alert('Editing error!');
        }
        this.facility = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getFacilities(): void {
    fetch('https://localhost:44381/api/facilities/getfacilities', {
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

  setFacility(id: number | null, facility: string): void {
    this.checkedFacility = id;
    this.facility = facility;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getFacilities();
  }

}
