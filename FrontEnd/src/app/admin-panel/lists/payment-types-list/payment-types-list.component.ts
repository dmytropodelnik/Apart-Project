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
  }

}
