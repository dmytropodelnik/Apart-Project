import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Payment } from 'src/app/models/Payment/payment.item';
import { PaymentType } from 'src/app/models/Payment/paymenttype.item';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-payments-list',
  templateUrl: './payments-list.component.html',
  styleUrls: ['./payments-list.component.css'],
})
export class PaymentsListComponent implements OnInit {
  payments: Payment[] | null = null;
  payment: PaymentType | null = null;
  checkedPayment: number | null = null;

  page: number = 1;
  pageSize: number = 15;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private adminContentService: AdminContentService,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  addPayment(): void {
    let payment = {
      name: this.payment,
    };

    fetch('https://localhost:44381/api/payments/addpayment', {
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
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
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
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  deletePayment(): void {
    let payment = {
      id: this.checkedPayment,
      name: this.payment,
    };

    fetch('https://localhost:44381/api/payments/deletepayment', {
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
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  getPayments(): void {
    fetch(
      `https://localhost:44381/api/payments/getpayments?page=${this.page}&pageSize=${this.pageSize}`,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.payments = data.payments;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  collectElements(payments: Payment[]): void {
    for (let item of payments) {
      this.payments?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(
      `https://localhost:44381/api/payments/getpayments?page=${this.page}&pageSize=${this.pageSize}`,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.payments);
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
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
