import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

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

  constructor(
    private modalService: NgbModal,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {}

  openVerticallyCentered(content: any, id: number, number: number) {
    this.modalService.open(content, {
      centered: true,
    });
    this.selectedBooking = id;
    this.numberBooking = number;
  }

  payBooking(): void {

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
          alert(response.message);
        }
      })
      .catch((ex) => {
        alert(ex);
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
          alert(response.message);
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }


  ngOnInit(): void {
    if (!AuthHelper.isLogged()) {
      this.router.navigate(['']);
    }
    this.getUserBooking();
  }
}
