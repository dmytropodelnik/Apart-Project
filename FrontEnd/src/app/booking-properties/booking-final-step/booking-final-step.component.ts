import { Component, OnInit } from '@angular/core';
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
  chosenSuggestion: any;

  grade: number = 0;
  diffDays: number = 0;

  checkIn: Date | null = null;
  checkOut: Date | null = null;

  discount: number = 0;
  price: number = 0;
  promoCode: string = '';

  address: string = '';
  city: string = '';
  country: string = '';
  zipCode: string = '';
  phone: string = '';

  finalPrice: number = 0;

  isPromoCodeApplied: boolean = false;

  applyPromoCode(): void {
    fetch(
      `https://localhost:44381/api/promocodes/confirmpromocode?promoCode=${this.promoCode}&price=${this.price}`,
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

  constructor(
    private bookingDetailsService: BookingDetailsService,
    ) {

     }

  ngOnInit(): void {
    this.chosenSuggestion = this.bookingDetailsService.getChosenSuggestion();
    this.grade = this.bookingDetailsService.getGrade();
    this.diffDays = this.bookingDetailsService.getDiffDays();
    this.checkIn = this.bookingDetailsService.getCheckInDate();
    this.checkOut = this.bookingDetailsService.getCheckOutDate();
  }

}
