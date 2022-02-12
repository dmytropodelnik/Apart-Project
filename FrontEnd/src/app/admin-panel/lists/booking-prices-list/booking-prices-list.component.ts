import { Component, OnInit } from '@angular/core';
import { BookingPrice } from 'src/app/models/Payment/bookingprice.item';

import AuthHelper from '../../../utils/authHelper';

@Component({
  selector: 'app-booking-prices-list',
  templateUrl: './booking-prices-list.component.html',
  styleUrls: ['./booking-prices-list.component.css']
})
export class BookingPricesListComponent implements OnInit {

  bookings: BookingPrice[] | null = null;
  booking: string | null = null;
  checkedBooking: number | null = null;

  constructor() {}

  addBooking(): void {
    let booking = {
      name: this.booking,
    };

    fetch('https://localhost:44381/api/bookingprices/addprice', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(booking),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getBookings();
        } else {
          alert('Adding error!');
        }
        this.booking = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editBooking(): void {
    let booking = {
      id: this.checkedBooking,
      name: this.booking,
    };

    fetch('https://localhost:44381/api/bookingprices/editprices', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(booking),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getBookings();
        } else {
          alert('Editing error!');
        }
        this.booking = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteBooking(): void {
    let bookings = {
      id: this.checkedBooking,
      name: this.bookings,
    };

    fetch('https://localhost:44381/api/bookingprices/deleteprice', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(bookings),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getBookings();
        } else {
          alert('Editing error!');
        }
        this.booking = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getBookings(): void {
    fetch('https://localhost:44381/api/bookingprices/getprices', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.bookings = data.bookings;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setBooking(id: number | null, booking: string): void {
    this.checkedBooking = id;
    this.booking = booking;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getBookings();
  }

}
