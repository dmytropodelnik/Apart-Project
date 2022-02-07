import { Component, OnInit } from '@angular/core';
import { CreditCard } from 'src/app/models/Payment/creditcard.item';

@Component({
  selector: 'app-credit-cards-list',
  templateUrl: './credit-cards-list.component.html',
  styleUrls: ['./credit-cards-list.component.css']
})
export class CreditCardsListComponent implements OnInit {

  creditCards: CreditCard[] | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}
