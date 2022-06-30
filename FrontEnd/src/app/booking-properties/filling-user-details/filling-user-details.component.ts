import { ConsoleLogger } from '@angular/compiler-cli/private/localize';
import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Guest } from 'src/app/models/UserData/guest.item';
import { BookingDetailsService } from 'src/app/services/booking-details.service';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../utils/authHelper';
import BookingHelper from '../../utils/bookingHelper';
import ImageHelper from '../../utils/imageHelper';
import MathHelper from '../../utils/mathHelper';

@Component({
  selector: 'app-filling-user-details',
  templateUrl: './filling-user-details.component.html',
  styleUrls: ['./filling-user-details.component.css'],
})
export class FillingUserDetailsComponent implements OnInit {
  chosenSuggestion: any;
  chosenApartments: {
    id: number;
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

  checkIn: string = '';
  checkOut: string = '';

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

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private router: Router,
    private activatedRouter: ActivatedRoute,
    private bookingDetailsService: BookingDetailsService,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  calculateTotalPrice(): void {
    for (let i = 0; i < this.chosenApartments.length; i++) {
      this.totalPrice += this.chosenApartments[i].priceInUSD;
    }
  }

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
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
      this.showAlert('Emails are not equal!');
      return;
    }
    if (this.mainFirstName.length < 1) {
      this.showAlert('Enter a first name!');
      return;
    }
    if (this.mainLastName.length < 1) {
      this.showAlert('Enter a last name!');
      return;
    }
    if (this.mainEmail.length < 1) {
      this.showAlert('Enter an email!');
      return;
    }

    for (let i = 0; i < this.guestsData.length; i++) {
      if (this.guestsData[i].length < 2) {
        this.showAlert('Enter a full guests name');
        return;
      }
    }

    this.bookingDetailsService.setGuestsData(this.guestsData);
    BookingHelper.saveGuestsData(this.guestsData);

    this.router.navigate(['/bookingfinalstep'], {
      queryParams: {
        totalPrice: this.totalPrice,
        isSaved: this.isSaved,
        email: this.mainEmail,
        isForWork: this.isForWork,
        firstName: this.mainFirstName,
        lastName: this.mainLastName,
        specialRequests: this.specialRequests,
      },
    });
  }

  fillApartmentsArray(): void {
    for (let i = 0; i < this.chosenApartments.length; i++) {
      this.guestsData.push('');
    }
  }

  getSuggestionCondition(): void {
    if (AuthHelper.isLogged()) {
      fetch(
        `https://apartmain.azurewebsites.net/api/favorites/issuggestionsaved?email=${AuthHelper.getLogin()}&id=${
          this.chosenSuggestion.id
        }`,
        {
          method: 'GET',
        }
      )
        .then((response) => response.json())
        .then((response) => {
          if (response.code === 200) {
            this.isSaved = response.result;
          } else {
          }
        })
        .catch((ex) => {
          this.mainDataService.alertContent = ex;
          this.modalService.open(this.alert);
        });
    }
  }

  ngOnInit(): void {
    if (BookingHelper.getBookingData()) {
      let bookingData = BookingHelper.getBookingData();
      this.chosenApartments = bookingData.chosenApartments as any;
      this.chosenSuggestion = bookingData.chosenSuggestion;
      this.grade = bookingData.bookingGrade;
      this.diffDays = +bookingData.bookingDiffDays;
      this.checkIn = bookingData.checkIn;
      this.checkOut = bookingData.checkOut;

      this.calculateTotalPrice();
      this.getSuggestionCondition();
      this.fillApartmentsArray();
    } else {
      this.router.navigate(['']);
    }
  }
}
