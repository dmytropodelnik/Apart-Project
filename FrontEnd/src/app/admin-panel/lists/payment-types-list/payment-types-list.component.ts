import { Component, OnInit } from '@angular/core';
import { PaymentType } from 'src/app/models/Payment/paymenttype.item';

@Component({
  selector: 'app-payment-types-list',
  templateUrl: './payment-types-list.component.html',
  styleUrls: ['./payment-types-list.component.css']
})
export class PaymentTypesListComponent implements OnInit {

  types: PaymentType[] | null = null;

  constructor() { }

  ngOnInit(): void {
    fetch('https://localhost:44381/api/countries/getcountries', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.types = data.types;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

}
