import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FlightBooking } from '../../../models/Services/flightbooking.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

import { MainDataService } from 'src/app/services/main-data.service';

@Component({
  selector: 'app-flight-bookings-list',
  templateUrl: './flight-bookings-list.component.html',
  styleUrls: ['./flight-bookings-list.component.css'],
})
export class FlightBookingsListComponent implements OnInit {
  bookings: FlightBooking[] | null = null;
  booking: string | null = null;
  checkedBooking: number | null = null;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  addBooking(): void {
    let booking = {
      name: this.booking,
    };

    fetch('https://localhost:44381/api/flights/addbooking', {
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
          this.getBookings();
        } else {
          alert('Adding error!');
        }
        this.booking = '';
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  editBooking(): void {
    let booking = {
      id: this.checkedBooking,
      name: this.booking,
    };

    fetch('https://localhost:44381/api/flights/editbooking', {
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
          this.getBookings();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.booking = '';
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  deleteBooking(): void {
    let booking = {
      id: this.checkedBooking,
      name: this.booking,
    };

    fetch('https://localhost:44381/api/flights/deletebooking', {
      method: 'DELETE',
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
          this.getBookings();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.booking = '';
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  getBookings(): void {
    fetch('https://localhost:44381/api/flights/getbookings', {
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
          this.bookings = data.bookings;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
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
