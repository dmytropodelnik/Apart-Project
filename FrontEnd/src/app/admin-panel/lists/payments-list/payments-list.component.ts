import { Component, OnInit } from '@angular/core';
import { Payment } from 'src/app/models/Payment/payment.item';

@Component({
  selector: 'app-payments-list',
  templateUrl: './payments-list.component.html',
  styleUrls: ['./payments-list.component.css']
})
export class PaymentsListComponent implements OnInit {

  payments: Payment[] | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}
