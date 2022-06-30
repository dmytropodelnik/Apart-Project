import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AirportTaxiBooking } from 'src/app/models/Services/airporttaxibooking.item';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-airport-taxi-bookings-list',
  templateUrl: './airport-taxi-bookings-list.component.html',
  styleUrls: ['./airport-taxi-bookings-list.component.css'],
})
export class AirportTaxiBookingsListComponent implements OnInit {
  bookings: AirportTaxiBooking[] | null = null;
  booking: string | null = null;
  checkedBooking: number | null = null;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  addBooking(): void {
    let booking = {
      name: this.booking,
    };

    fetch('https://localhost:44381/api/airporttaxis/addbooking', {
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
          this.showAlert('Adding error!');
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

    fetch('https://localhost:44381/api/airporttaxis/editbooking', {
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
          this.showAlert('Editing error!');
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

    fetch('https://localhost:44381/api/airporttaxis/deletebooking', {
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
          this.showAlert('Editing error!');
        }
        this.booking = '';
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  getBookings(): void {
    fetch('https://localhost:44381/api/airporttaxis/getbookings', {
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
          this.showAlert('Fetch error!');
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
