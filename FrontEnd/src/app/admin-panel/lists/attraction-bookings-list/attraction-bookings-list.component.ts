import { Component, OnInit } from '@angular/core';
import { AttractionBooking } from 'src/app/models/Services/attractionbooking.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-attraction-bookings-list',
  templateUrl: './attraction-bookings-list.component.html',
  styleUrls: ['./attraction-bookings-list.component.css']
})
export class AttractionBookingsListComponent implements OnInit {

  bookings: AttractionBooking[] | null = null;
  booking: string | null = null;
  checkedBooking: number | null = null;

  constructor() {}

  addBooking(): void {
    let booking = {
      name: this.booking,
    };

    fetch('http://apartmain.azurewebsites.net/api/attractions/addbooking', {
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

    fetch('http://apartmain.azurewebsites.net/api/attractions/editbooking', {
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
          ListHelper.disableButtons();
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

    fetch('http://apartmain.azurewebsites.net/api/attractions/deletebooking', {
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
          ListHelper.disableButtons();
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
    fetch('http://apartmain.azurewebsites.net/api/attractions/getbookings', {
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
