import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MainDataService } from '../services/main-data.service';

import AuthHelper from '../utils/authHelper';
import ImageHelper from '../utils/imageHelper';
import MathHelper from '../utils/mathHelper';

@Component({
  selector: 'app-user-bookings',
  templateUrl: './user-bookings.component.html',
  styleUrls: ['./user-bookings.component.css'],
})
export class UserBookingsComponent implements OnInit {
  bookings: any[] = [];

  imageHelper: any = ImageHelper;

  selectedBooking: number = 1;
  numberBooking: number = 0;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  openVerticallyCentered(content: any, id: number, number: number) {
    this.modalService.open(content, {
      centered: true,
    });
    this.selectedBooking = id;
    this.numberBooking = number;
  }

  payBooking(): void {}

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  getUserBooking(): void {
    fetch(
      `https://apartmain.azurewebsites.net/api/staybookings/getuserbookings?email=${AuthHelper.getLogin()}`,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.bookings = response.stayBookings;
        } else {
          this.showAlert(response.message);
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  deleteBooking(): void {
    fetch(
      `https://apartmain.azurewebsites.net/api/staybookings/deletebooking?id=${this.selectedBooking}`,
      {
        method: 'DELETE',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.getUserBooking();
        } else {
          this.showAlert(response.message);
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  ngOnInit(): void {
    if (!AuthHelper.isLogged()) {
      this.router.navigate(['']);
    }
    this.getUserBooking();
  }
}
