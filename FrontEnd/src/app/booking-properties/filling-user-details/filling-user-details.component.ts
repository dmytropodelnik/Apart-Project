import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Guest } from 'src/app/models/UserData/guest.item';
import { BookingDetailsService } from 'src/app/services/booking-details.service';

import AuthHelper from '../../utils/authHelper';
import ImageHelper from '../../utils/imageHelper';
import MathHelper from '../../utils/mathHelper';

@Component({
  selector: 'app-filling-user-details',
  templateUrl: './filling-user-details.component.html',
  styleUrls: ['./filling-user-details.component.css']
})
export class FillingUserDetailsComponent implements OnInit {
  chosenSuggestion: any;
  chosenApartments: {
    name: string;
    amount: number;
    roomsAmount: number;
    guestsLimit: number;
    bathroomsAmount: number;
    apartmentSize: number;
    priceInUSD: number;
    isSuite: string;
    isSmokingAllowed: boolean;
  }[] = [];

  grade: number = 0;
  diffDays: number = 0;

  checkIn: Date | null = null;
  checkOut: Date | null = null;

  totalPrice: number = 0;

  mathHelper: any = MathHelper;

  isSaved: boolean = false;
  isForWork: boolean = false;

  mainFirstName: string = '';
  mainLastName: string = '';
  mainEmail: string = '';
  confirmEmail: string = '';
  specialRequests: string = '';

  guestsData: string[] = [];

  constructor(
    private router: Router,
    private activatedRouter: ActivatedRoute,
    private bookingDetailsService: BookingDetailsService,
    ) {

    }

  calculateTotalPrice(): void {
    for (let i = 0; i < this.chosenApartments.length; i++) {
      this.totalPrice += this.chosenApartments[i].priceInUSD;
    }
  }

  changeIsForWork(): void {
    this.isForWork = !this.isForWork;
  }

  showSuggestion(uniqueCode: number): void {
    this.router.navigate(['suggestion', uniqueCode], {
      queryParams: {
        isSaved: this.isSaved,
      },
    });
  }

  continueBooking(): void {
    if (this.mainEmail != this.confirmEmail) {
      alert("Emails are not equal!");
      return;
    }
    if (this.mainFirstName.length < 1) {
      alert('Enter a first name!');
      return;
    }
    if (this.mainLastName.length < 1) {
      alert('Enter a last name!');
      return;
    }
    if (this.mainEmail.length < 1) {
      alert('Enter an email!');
      return;
    }

    for (let i = 0; i < this.guestsData.length; i++) {
      if (this.guestsData[i].length < 2) {
        alert('Enter a full guests name');
        return;
      }
    }

    this.bookingDetailsService.setGuestsData(this.guestsData);

    this.router.navigate(['/bookingfinalstep'], {
      queryParams: {
        totalPrice: this.totalPrice,
        isSaved: this.isSaved,
        email: this.mainEmail,
        isForWork: this.isForWork,
        firstName: this.mainFirstName,
        lastName: this.mainLastName,
      },
    })
  }

  fillApartmentsArray(): void {
    for (let i = 0; i < this.chosenApartments.length; i++) {
      this.guestsData.push('');
    }
  }

  getSuggestionCondition(): void {
    if (AuthHelper.isLogged()) {
      fetch(
        `https://localhost:44381/api/favorites/issuggestionsaved?email=${AuthHelper.getLogin()}&id=${this.chosenSuggestion.id}`,
        {
          method: 'GET',
        }
      )
        .then((response) => response.json())
        .then((response) => {
          if (response.code === 200) {
            this.isSaved = response.result;
          } else {
            alert(response.message);
          }
        })
        .catch((ex) => {
          alert(ex);
        });
    }
  }

  ngOnInit(): void {
    this.chosenApartments = this.bookingDetailsService.getChosenApartments();
    this.chosenSuggestion = this.bookingDetailsService.getChosenSuggestion();
    this.grade = this.bookingDetailsService.getGrade();
    this.diffDays = this.bookingDetailsService.getDiffDays();
    this.checkIn = this.bookingDetailsService.getCheckInDate();
    this.checkOut = this.bookingDetailsService.getCheckOutDate();

    this.calculateTotalPrice();
    this.getSuggestionCondition();
    this.fillApartmentsArray();
    // if (this.bookingDetailsService.getChosenApartments() != null) {
    //   this.chosenApartments = this.bookingDetailsService.getChosenApartments();
    //   this.chosenSuggestion = this.bookingDetailsService.getChosenSuggestion();
    //   console.log(this.chosenSuggestion);
    // } else {
    //   this.router.navigate(['']);
    // }
  }

}
