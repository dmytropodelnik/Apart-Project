import { Component, OnInit } from '@angular/core';
import { City } from 'src/app/models/Location/city.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';
import ImageHelper from '../../../utils/imageHelper';
import { AdminContentService } from 'src/app/services/admin-content.service';


@Component({
  selector: 'app-cities-list',
  templateUrl: './cities-list.component.html',
  styleUrls: ['./cities-list.component.css']
})
export class CitiesListComponent implements OnInit {

  cities: City[] | null = null;
  city: City | null = null;
  searchCity: string = '';
  checkedCity: number | null = null;
  imageHelper: any = ImageHelper;

  page: number = 1;
  pageSize: number = 15;

  constructor(
    private adminContentService: AdminContentService
  ) {

  }

  search(): void {
    fetch('https://apartmain.azurewebsites.net/api/cities/search?city=' + this.searchCity, {
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
          this.cities = data.cities;
        } else {
          alert('Search error!');
        }
        this.searchCity = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addCity(): void {
    let city = {
      name: this.city,
    };

    fetch('https://apartmain.azurewebsites.net/api/cities/addcity', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
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
        this.city = null;
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

    fetch('https://apartmain.azurewebsites.net/api/cities/editcity', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(city),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCities();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.city = null;
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

    fetch('https://apartmain.azurewebsites.net/api/cities/deletecity', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(city),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCities();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.city = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getCities(): void {
    fetch(`https://apartmain.azurewebsites.net/api/cities/getcities?page=${this.page}&pageSize=${this.pageSize}`, {
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
          this.cities = data.cities;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  collectElements(cities: City[]): void {
    for (let item of cities) {
      this.cities?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(`https://apartmain.azurewebsites.net/api/cities/getcities?page=${this.page}&pageSize=${this.pageSize}`, {
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
          this.collectElements(data.cities);
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setCity(city: City): void {
    this.checkedCity = city.id;
    this.city = city;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getCities();
  }

}
