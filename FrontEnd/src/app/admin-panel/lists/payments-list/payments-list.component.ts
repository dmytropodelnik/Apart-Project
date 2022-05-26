import { Component, OnInit } from '@angular/core';
import { Payment } from 'src/app/models/Payment/payment.item';
import { PaymentType } from 'src/app/models/Payment/paymenttype.item';
import { AdminContentService } from 'src/app/services/admin-content.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-payments-list',
  templateUrl: './payments-list.component.html',
  styleUrls: ['./payments-list.component.css']
})
export class PaymentsListComponent implements OnInit {

  payments: Payment[] | null = null;
  payment: PaymentType | null = null;
  checkedPayment: number | null = null;

  page: number = 1;
  pageSize: number = 15;

  constructor(
    private adminContentService: AdminContentService
  ) {

  }

  addPayment(): void {
    let payment = {
      name: this.payment,
    };

    fetch('https://apartmain.azurewebsites.net/api/payments/addpayment', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
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
        this.payment = null;
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

    fetch('https://apartmain.azurewebsites.net/api/payments/editpayment', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(payment),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getPayments();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.payment = null;
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

    fetch('https://apartmain.azurewebsites.net/api/payments/deletepayment', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(payment),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getPayments();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.payment = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getPayments(): void {
    fetch(`https://apartmain.azurewebsites.net/api/payments/getpayments?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
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

  collectElements(payments: Payment[]): void {
    for (let item of payments) {
      this.payments?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(`https://apartmain.azurewebsites.net/api/payments/getpayments?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.payments);
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setPayment(id: number | null, payment: PaymentType | null): void {
    this.checkedPayment = id;
    this.payment = payment;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getPayments();
  }

}
