import { Component, OnInit } from '@angular/core';
import { Facility } from 'src/app/models/facility.item';
import { FacilityType } from 'src/app/models/facilitytype.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';
import ImageHelper from '../../../utils/imageHelper';


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

  constructor() {
    this.facility = new Facility();
  }

  search(): void {
    fetch('https://localhost:44381/api/facilities/search?facility=' + this.searchFacility, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
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
      image: null,
      facilityTypeId: this.facility.facilityType?.id,
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
      facilityType: this.facility.facilityType?.id,
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
      facilityType: this.facility.facilityType?.id,
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
