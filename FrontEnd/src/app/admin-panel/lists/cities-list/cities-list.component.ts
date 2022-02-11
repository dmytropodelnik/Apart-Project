import { Component, OnInit } from '@angular/core';
import { City } from 'src/app/models/Location/city.item';

import AuthHelper from '../../../utils/authHelper';

@Component({
  selector: 'app-cities-list',
  templateUrl: './cities-list.component.html',
  styleUrls: ['./cities-list.component.css']
})
export class CitiesListComponent implements OnInit {

  cities: City[] | null = null;
  city: string | null = null;
  checkedCity: number | null = null;

  constructor() {}

  addCity(): void {
    let city = {
      name: this.city,
    };

    fetch('https://localhost:44381/api/cities/addcity', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(city),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCities();
        } else {
          alert('Adding error!');
        }
        this.city = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editCity(): void {
    let city = {
      id: this.checkedCity,
      name: this.city,
    };

    fetch('https://localhost:44381/api/cities/editcity', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(city),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCities();
        } else {
          alert('Editing error!');
        }
        this.city = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteCity(): void {
    let city = {
      id: this.checkedCity,
      name: this.city,
    };

    fetch('https://localhost:44381/api/cities/deletecity', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(city),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCities();
        } else {
          alert('Editing error!');
        }
        this.city = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getCities(): void {
    fetch('https://localhost:44381/api/cities/getcities', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.cities = data.cities;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setCity(id: number | null, city: string): void {
    this.checkedCity = id;
    this.city = city;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getCities();
  }

}
