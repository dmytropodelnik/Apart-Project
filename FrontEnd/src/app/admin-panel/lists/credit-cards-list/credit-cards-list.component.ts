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
    fetch('https://localhost:44381/api/countries/getcountries', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.creditCards = data.creditCards;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

}
