import { Component, OnInit } from '@angular/core';
import { Country } from 'src/app/models/Location/country.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';
import ImageHelper from '../../../utils/imageHelper';
import { AdminContentService } from 'src/app/services/admin-content.service';

@Component({
  selector: 'app-countries-list',
  templateUrl: './countries-list.component.html',
  styleUrls: ['./countries-list.component.css'],
})
export class CountriesListComponent implements OnInit {

  countries: Country[] | null = null;
  country: Country | null = null;
  searchCountry: string = '';
  checkedCountry: number | null = null;
  imageHelper: any = ImageHelper;

  page: number = 1;
  pageSize: number = 15;

  constructor(
    private adminContentService: AdminContentService
  ) {

  }

  search(): void {
    fetch('https://apartmain.azurewebsites.net/api/countries/search?country=' + this.searchCountry, {
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
          this.countries = data.countries;
        } else {
          alert('Search error!');
        }
        this.searchCountry = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addCountry(): void {
    let country = {
      name: this.country,
    };

    fetch('https://apartmain.azurewebsites.net/api/countries/addcountry', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(country),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCountries();
        } else {
          alert('Adding error!');
        }
        this.country = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editCountry(): void {
    let country = {
      id: this.checkedCountry,
      name: this.country,
    };

    fetch('https://apartmain.azurewebsites.net/api/countries/editcountry', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(country),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCountries();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.country = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteCountry(): void {
    let country = {
      id: this.checkedCountry,
      name: this.country,
    };

    fetch('https://apartmain.azurewebsites.net/api/countries/deletecountry', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(country),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCountries();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.country = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getCountries(): void {
    fetch(`https://apartmain.azurewebsites.net/api/countries/getcountries?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.countries = data.countries;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  collectElements(countries: Country[]): void {
    for (let item of countries) {
      this.countries?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(`https://apartmain.azurewebsites.net/api/countries/getcountries?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.countries);
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setCountry(country: Country): void {
    this.checkedCountry = country.id;
    this.country = country;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getCountries();
  }
}
