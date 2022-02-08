import { Component, OnInit } from '@angular/core';
import { Currency } from 'src/app/models/Payment/currency.item';

@Component({
  selector: 'app-currencies-list',
  templateUrl: './currencies-list.component.html',
  styleUrls: ['./currencies-list.component.css']
})
export class CurrenciesListComponent implements OnInit {

  currencies: Currency[] | null = null;

  constructor() { }

  ngOnInit(): void {
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

}
