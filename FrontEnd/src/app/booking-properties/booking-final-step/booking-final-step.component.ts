import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Guest } from 'src/app/models/UserData/guest.item';
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

  firstName: string = '';
  lastName: string = '';

  email: string = '';
  specialRequests: string = '';

  address: string = '';
  city: string = '';
  country: string = '';
  zipCode: string = '';
  phone: string = '';

  guestsData: Guest[] = [];

  finalPrice: number = 0;
  difference: number = 0;

  isSaved: boolean = false;
  isForWork: boolean = false;
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
      `https://localhost:44381/api/promocodes/confirmpromocode?promoCode=${this.promoCode}&price=${this.totalPrice}`,
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
          this.discount = response.discount;
          this.difference = response.difference;
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
    const booking = {
      suggestionId: this.chosenSuggestion.id,
      discount: this.discount,
      totalPrice: this.totalPrice,
      finalPrice: this.finalPrice,
      difference: this.difference,
      isForWork: this.isForWork,
      checkIn: this.checkIn,
      checkOut: this.checkOut,
      specialRequests: this.specialRequests,
      promoCode: this.promoCode,
      customerEmail: this.email,
      userEmail: AuthHelper.getLogin(),
      addressText: this.address,
      city: this.city,
      country: this.country,
      phoneNumber: this.phone,
      zipCode: this.zipCode,
      guests: this.guestsData,
    };

    fetch(
      `https://localhost:44381/api/staybookings/addstaybooking`,
      {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
        body: JSON.stringify(booking),
      }
    )
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.isPromoCodeApplied = true;
          this.finalPrice = response.finalPrice;
          this.discount = response.discount;
          this.difference = response.difference;
        } else {
          alert(response.message);
        }
      })
      .catch((ex) => {
        alert(ex);
      });

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
    this.guestsData = this.bookingDetailsService.getGuestsData();

    this.activatedRouter.queryParams.subscribe((params: any) => {
      if (params['totalPrice']) {
        this.totalPrice = params['totalPrice'];
      }
      if (params['isSaved']) {
        this.isSaved = params['isSaved'];
      }
      if (params['email']) {
        this.email = params['email'];
      }
      if (params['isForWork']) {
        this.isForWork = params['isForWork'];
      }
      if (params['firstName']) {
        this.firstName = params['firstName'];
      }
      if (params['lastName']) {
        this.lastName = params['lastName'];
      }
    });
  }

}
