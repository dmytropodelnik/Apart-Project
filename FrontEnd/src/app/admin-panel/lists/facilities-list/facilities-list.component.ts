import { Component, OnInit } from '@angular/core';
import { Facility } from 'src/app/models/facility.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-facilities-list',
  templateUrl: './facilities-list.component.html',
  styleUrls: ['./facilities-list.component.css'],
})
export class FacilitiesListComponent implements OnInit {
  facilities: Facility[] | null = null;
  facility: Facility;
  checkedFacility: number | null = null;

  constructor() {
    this.facility = new Facility();
  }

  addFacility(): void {
    let facility = {
      text: this.facility.text,
      image: null,
      facilityType: null,
      suggestion: null,
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
      image: null,
      facilityType: null,
      suggestion: null,
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
      image: null,
      facilityType: null,
      suggestion: null,
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

  resetFacility(): void {
    this.facility.text = '';
    this.facility.image = null;
    this.facility.facilityType = null;
    this.facility.suggestion = null;
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

  setFacility(facility: Facility): void {
    this.checkedFacility = facility.id;
    this.facility.text = facility.text;
    this.facility.image = facility.image;
    this.facility.facilityType = facility.facilityType;
    this.facility.suggestion = facility.suggestion;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  getFacilityTypes(): void {
    fetch('https://localhost:44381/api/facilitytypes/gettypes', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          let types = document.getElementById('facilityTypes');
          let newOption;
          for (let type of data.types) {
            newOption = new Option(type.type, type.type);
            types?.append(newOption);
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
