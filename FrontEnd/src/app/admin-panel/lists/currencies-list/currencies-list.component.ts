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
  }

}
