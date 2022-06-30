import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Currency } from 'src/app/models/Payment/currency.item';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-currencies-list',
  templateUrl: './currencies-list.component.html',
  styleUrls: ['./currencies-list.component.css'],
})
export class CurrenciesListComponent implements OnInit {
  currencies: Currency[] | null = null;
  currency: Currency;
  searchCurrency: string = '';
  checkedCurrency: number | null = null;

  page: number = 1;
  pageSize: number = 15;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private adminContentService: AdminContentService,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {
    this.currency = new Currency();
  }

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  search(): void {
    fetch(
      'https://apartmain.azurewebsites.net/api/currencies/search?currency=' +
        this.searchCurrency,
      {
        method: 'GET',
        headers: {
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.currencies = data.currencies;
        } else {
          this.showAlert('Search error!');
        }
        this.searchCurrency = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  addCurrency(): void {
    let currency = {
      value: this.currency.value,
      abbreviation: this.currency.abbreviation,
      bankCode: this.currency.bankCode,
    };

    fetch('https://apartmain.azurewebsites.net/api/currencies/addcurrency', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(currency),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCurrencies();
        } else {
          this.showAlert('Adding error!');
        }
        this.currency.value = '';
        this.currency.abbreviation = '';
        this.currency.bankCode = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  editCurrency(): void {
    let currency = {
      id: this.checkedCurrency,
      value: this.currency.value,
      abbreviation: this.currency.abbreviation,
      bankCode: this.currency.bankCode,
    };

    fetch('https://apartmain.azurewebsites.net/api/currencies/editcurrency', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(currency),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCurrencies();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.currency.value = '';
        this.currency.abbreviation = '';
        this.currency.bankCode = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  deleteCurrency(): void {
    let currency = {
      id: this.checkedCurrency,
      value: this.currency.value,
      abbreviation: this.currency.abbreviation,
      bankCode: this.currency.bankCode,
    };

    fetch('https://apartmain.azurewebsites.net/api/currencies/deletecurrency', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(currency),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCurrencies();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.currency.value = '';
        this.currency.abbreviation = '';
        this.currency.bankCode = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  getCurrencies(): void {
    fetch(
      `https://apartmain.azurewebsites.net/api/currencies/getcurrencies?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.currencies = data.currencies;
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  collectElements(currencies: Currency[]): void {
    for (let item of currencies) {
      this.currencies?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(
      `https://apartmain.azurewebsites.net/api/currencies/getcurrencies?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.collectElements(data.currencies);
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  setCurrency(currency: Currency): void {
    this.checkedCurrency = currency.id;
    this.currency.value = currency.value;
    this.currency.abbreviation = currency.abbreviation;
    this.currency.bankCode = currency.bankCode;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getCurrencies();
  }
}
