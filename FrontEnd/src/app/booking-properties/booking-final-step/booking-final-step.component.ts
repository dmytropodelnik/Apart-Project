import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BookingDetailsService } from 'src/app/services/booking-details.service';

import AuthHelper from '../../utils/authHelper';
import ImageHelper from '../../utils/imageHelper';
import MathHelper from '../../utils/mathHelper';

@Component({
  selector: 'app-booking-final-step',
  templateUrl: './booking-final-step.component.html',
  styleUrls: ['./booking-final-step.component.css']
})
export class BookingFinalStepComponent implements OnInit {
  mathHelper: any = MathHelper;

  chosenSuggestion: any;

  grade: number = 0;
  diffDays: number = 0;

  checkIn: Date | null = null;
  checkOut: Date | null = null;

  discount: number = 0;
  totalPrice: number = 0;
  promoCode: string = '';

  address: string = '';
  city: string = '';
  country: string = '';
  zipCode: string = '';
  phone: string = '';

  finalPrice: number = 0;

  isSaved: boolean = false;
  isPromoCodeApplied: boolean = false;

  constructor(
    private router: Router,
    private activatedRouter: ActivatedRoute,
    private bookingDetailsService: BookingDetailsService,
    ) {

     }

  applyPromoCode(): void {
    if (this.promoCode.length < 6) {
      alert('Please enter a correct promo code!');
      return;
    }

    fetch(
      `https://apartmain.azurewebsites.net/api/promocodes/confirmpromocode?promoCode=${this.promoCode}&price=${this.totalPrice}`,
      {
        method: 'PUT',
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
          this.isPromoCodeApplied = true;
          this.finalPrice = response.finalPrice;
        } else {
          alert(response.message);
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  completeBooking(): void {
    if (this.address.length < 5) {
      alert("Address must contain at least 5 characters!");
      return;
    }
    if (this.city.length < 2) {
      alert("City must contain at least 2 characters!");
      return;
    }
    if (this.country.length < 2) {
      alert("Country must contain at least 2 characters!");
      return;
    }
    if (!this.phone.match('^[+]?[(]?[0-9]{3}[)]?[-s.]?[0-9]{3}[-s.]?[0-9]{4,6}$')) {
      alert("Enter a correct phone number!");
      return;
    }

    this.registerBooking();
  }

  registerBooking(): void {
    if (AuthHelper.isLogged()) {
      this.router.navigate(['/viewproperties']);
    } else {
      this.showSuccessBooking();
    }
  }

  showSuccessBooking(): void {

  }

  showSuggestion(uniqueCode: number): void {
    this.router.navigate(['suggestion', uniqueCode], {
      queryParams: {
        isSaved: this.isSaved,
      },
    });
  }

  ngOnInit(): void {
    this.chosenSuggestion = this.bookingDetailsService.getChosenSuggestion();
    this.grade = this.bookingDetailsService.getGrade();
    this.diffDays = this.bookingDetailsService.getDiffDays();
    this.checkIn = this.bookingDetailsService.getCheckInDate();
    this.checkOut = this.bookingDetailsService.getCheckOutDate();

    this.activatedRouter.queryParams.subscribe((params: any) => {
      if (params['totalPrice']) {
        this.totalPrice = params['totalPrice'];
      }
      if (params['isSaved']) {
        this.isSaved = params['isSaved'];
      }
    });
  }

}
