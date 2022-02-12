import { Component, OnInit } from '@angular/core';
import { AirportTaxiBooking } from 'src/app/models/Services/airporttaxibooking.item';

import AuthHelper from '../../../utils/authHelper';

@Component({
  selector: 'app-airport-taxi-bookings-list',
  templateUrl: './airport-taxi-bookings-list.component.html',
  styleUrls: ['./airport-taxi-bookings-list.component.css']
})
export class AirportTaxiBookingsListComponent implements OnInit {

  bookings: AirportTaxiBooking[] | null = null;
  booking: string | null = null;
  checkedBooking: number | null = null;

  constructor() {}

  addBooking(): void {
    let booking = {
      name: this.booking,
    };

    fetch('https://localhost:44381/api/airporttaxibookings/addbooking', {
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

    fetch('https://localhost:44381/api/airporttaxibookings/editbooking', {
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
    let booking = {
      id: this.checkedBooking,
      name: this.booking,
    };

    fetch('https://localhost:44381/api/airporttaxibookings/deletebooking', {
      method: 'DELETE',
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

  getBookings(): void {
    fetch('https://localhost:44381/api/airporttaxibookings/getbookings', {
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
