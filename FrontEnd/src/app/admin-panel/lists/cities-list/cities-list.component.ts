import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { City } from 'src/app/models/Location/city.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';
import ImageHelper from '../../../utils/imageHelper';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-cities-list',
  templateUrl: './cities-list.component.html',
  styleUrls: ['./cities-list.component.css'],
})
export class CitiesListComponent implements OnInit {
  cities: City[] | null = null;
  city: City | null = null;
  searchCity: string = '';
  checkedCity: number | null = null;
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
      'https://apartmain.azurewebsites.net/api/cities/search?city=' +
        this.searchCity,
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
          this.cities = data.cities;
        } else {
          this.showAlert('Search error!');
        }
        this.searchCity = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
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
          this.showAlert('Adding error!');
        }
        this.city = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
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
          this.showAlert('Editing error!');
        }
        this.city = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
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
          this.showAlert('Editing error!');
        }
        this.city = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  getCities(): void {
    fetch(
      `https://apartmain.azurewebsites.net/api/cities/getcities?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.cities = data.cities;
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  collectElements(cities: City[]): void {
    for (let item of cities) {
      this.cities?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(
      `https://apartmain.azurewebsites.net/api/cities/getcities?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.collectElements(data.cities);
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
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
