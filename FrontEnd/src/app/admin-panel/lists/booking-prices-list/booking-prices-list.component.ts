import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BookingPrice } from 'src/app/models/Payment/bookingprice.item';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-booking-prices-list',
  templateUrl: './booking-prices-list.component.html',
  styleUrls: ['./booking-prices-list.component.css'],
})
export class BookingPricesListComponent implements OnInit {
  prices: BookingPrice[] | null = null;
  price: BookingPrice | null = null;
  checkedBooking: number | null = null;

  page: number = 1;
  pageSize: number = 10;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private adminContentService: AdminContentService,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  addPrice(): void {
    let booking = {
      name: this.price,
    };

    fetch('https://apartmain.azurewebsites.net/api/bookingprices/addprice', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(booking),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getPrices();
        } else {
          this.showAlert('Adding error!');
        }
        this.price = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  editPrice(): void {
    let booking = {
      id: this.checkedBooking,
      name: this.price,
    };

    fetch('https://apartmain.azurewebsites.net/api/bookingprices/editprices', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(booking),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getPrices();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.price = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  deletePrice(): void {
    let bookings = {
      id: this.checkedBooking,
      name: this.price,
    };

    fetch('https://apartmain.azurewebsites.net/api/bookingprices/deleteprice', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(bookings),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getPrices();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.price = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  getPrices(): void {
<<<<<<< HEAD
    fetch(`https://apartmain.azurewebsites.net/api/bookingprices/getprices?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
=======
    fetch(
      `https://localhost:44381/api/bookingprices/getprices?page=${this.page}&pageSize=${this.pageSize}`,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
>>>>>>> backend
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.prices = data.bookings;
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  collectElements(prices: BookingPrice[]): void {
    for (let item of prices) {
      this.prices?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

<<<<<<< HEAD
    fetch(`https://apartmain.azurewebsites.net/api/bookingprices/getprices?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
=======
    fetch(
      `https://localhost:44381/api/bookingprices/getprices?page=${this.page}&pageSize=${this.pageSize}`,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
>>>>>>> backend
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.bookings);
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  setPrice(price: BookingPrice): void {
    this.checkedBooking = price.id;
    this.price = price;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getPrices();
  }
}
