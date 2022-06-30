import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AttractionBooking } from 'src/app/models/Services/attractionbooking.item';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-attraction-bookings-list',
  templateUrl: './attraction-bookings-list.component.html',
  styleUrls: ['./attraction-bookings-list.component.css'],
})
export class AttractionBookingsListComponent implements OnInit {
  bookings: AttractionBooking[] | null = null;
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

    fetch('https://localhost:44381/api/attractions/addbooking', {
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

    fetch('https://localhost:44381/api/attractions/editbooking', {
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

    fetch('https://localhost:44381/api/attractions/deletebooking', {
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
    fetch('https://localhost:44381/api/attractions/getbookings', {
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
