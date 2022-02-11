import { Component, OnInit } from '@angular/core';
import { Payment } from 'src/app/models/Payment/payment.item';

import AuthHelper from '../../../utils/authHelper';

@Component({
  selector: 'app-payments-list',
  templateUrl: './payments-list.component.html',
  styleUrls: ['./payments-list.component.css']
})
export class PaymentsListComponent implements OnInit {

  payments: Payment[] | null = null;
  payment: string | null = null;
  checkedPayment: number | null = null;

  constructor() {}

  addPayment(): void {
    let payment = {
      name: this.payment,
    };

    fetch('https://localhost:44381/api/payments/addpayment', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(payment),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getPayments();
        } else {
          alert('Adding error!');
        }
        this.payment = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editPayment(): void {
    let payment = {
      id: this.checkedPayment,
      name: this.payment,
    };

    fetch('https://localhost:44381/api/payments/editpayment', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(payment),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getPayments();
        } else {
          alert('Editing error!');
        }
        this.payment = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deletePayment(): void {
    let payment = {
      id: this.checkedPayment,
      name: this.payment,
    };

    fetch('https://localhost:44381/api/payments/deletepayment', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(payment),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getPayments();
        } else {
          alert('Editing error!');
        }
        this.payment = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getPayments(): void {
    fetch('https://localhost:44381/api/payments/getpayments', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.payments = data.payments;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setPayment(id: number | null, payment: string): void {
    this.checkedPayment = id;
    this.payment = payment;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getPayments();
  }

}
