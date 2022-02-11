import { Component, OnInit } from '@angular/core';
import { Currency } from 'src/app/models/Payment/currency.item';

import AuthHelper from '../../../utils/authHelper';

@Component({
  selector: 'app-currencies-list',
  templateUrl: './currencies-list.component.html',
  styleUrls: ['./currencies-list.component.css']
})
export class CurrenciesListComponent implements OnInit {

  currencies: Currency[] | null = null;
  currency: string | null = null;
  checkedCurrency: number | null = null;

  constructor() {}

  addCurrency(): void {
    let currency = {
      name: this.currency,
    };

    fetch('https://localhost:44381/api/currencies/addcurrency', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(currency),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCurrencies();
        } else {
          alert('Adding error!');
        }
        this.currency = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editCurrency(): void {
    let currency = {
      id: this.checkedCurrency,
      name: this.currency,
    };

    fetch('https://localhost:44381/api/currencies/editcurrency', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(currency),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCurrencies();
        } else {
          alert('Editing error!');
        }
        console.log(data);
        this.currency = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteCurrency(): void {
    let currency = {
      id: this.checkedCurrency,
      name: this.currency,
    };

    fetch('https://localhost:44381/api/currencies/deletecurrency', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(currency),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCurrencies();
        } else {
          alert('Editing error!');
        }
        this.currency = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getCurrencies(): void {
    fetch('https://localhost:44381/api/currencies/getcurrencies', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.currencies = data.currencies;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setCurrency(id: number | null, currency: string): void {
    this.checkedCurrency = id;
    this.currency = currency;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getCurrencies();
  }

}
