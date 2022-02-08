import { Component, OnInit } from '@angular/core';
import { CardType } from 'src/app/models/Payment/cardtype.item';

@Component({
  selector: 'app-card-types-list',
  templateUrl: './card-types-list.component.html',
  styleUrls: ['./card-types-list.component.css']
})
export class CardTypesListComponent implements OnInit {

  cardTypes: CardType[] | null = null;

  constructor() { }

  ngOnInit(): void {
    fetch('https://localhost:44381/api/countries/getcountries', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.cardTypes = data.cardTypes;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

}
