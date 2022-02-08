import { Component, OnInit } from '@angular/core';
import { PromoCode } from 'src/app/models/Payment/promocode.item';

@Component({
  selector: 'app-promo-codes-list',
  templateUrl: './promo-codes-list.component.html',
  styleUrls: ['./promo-codes-list.component.css']
})
export class PromoCodesListComponent implements OnInit {

  codes: PromoCode[] | null = null;

  constructor() { }

  ngOnInit(): void {
    fetch('https://localhost:44381/api/countries/getcountries', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.codes = data.codes;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

}
