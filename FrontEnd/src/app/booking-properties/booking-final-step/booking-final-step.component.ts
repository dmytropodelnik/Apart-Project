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
  selector: 'app-booking-final-step',
  templateUrl: './booking-final-step.component.html',
  styleUrls: ['./booking-final-step.component.css'],
})
export class BookingFinalStepComponent implements OnInit {
  mathHelper: any = MathHelper;
  imageHelper: any = ImageHelper;

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

  apartmentsIds: number[] = [];

  grade: number = 0;
  diffDays: number = 0;

  checkIn: string = '';
  checkOut: string = '';

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

  letterMessage: string = '';

  guestsData: string[] = [];

  finalPrice: number = 0;
  difference: number = 0;

  isSaved: boolean = false;
  isForWork: boolean = false;
  isPromoCodeApplied: boolean = false;

  newStayBooking: any;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private router: Router,
    private modalService: NgbModal,
    private activatedRouter: ActivatedRoute,
    private bookingDetailsService: BookingDetailsService,
    public mainDataService: MainDataService
  ) {}

  openVerticallyCentered(content: any) {
    this.modalService.open(content, {
      centered: true,
    });
  }

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  applyPromoCode(): void {
    if (this.promoCode.length < 6) {
      this.showAlert('Please enter a correct promo code!');
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
          this.discount = response.discount;
          this.difference = response.difference;
        } else {
          this.showAlert(response.message);
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  completeBooking(revealContent: any): void {
    if (this.address.length < 5) {
      this.showAlert('Address must contain at least 5 characters!');
      return;
    }
    if (this.city.length < 2) {
      this.showAlert('City must contain at least 2 characters!');
      return;
    }
    if (this.country.length < 2) {
      this.showAlert('Country must contain at least 2 characters!');
      return;
    }
    if (
      !this.phone.match('^[+]?[(]?[0-9]{3}[)]?[-s.]?[0-9]{3}[-s.]?[0-9]{4,6}$')
    ) {
      this.showAlert('Enter a correct phone number!');
      return;
    }

    this.registerBooking(revealContent);
  }

  registerBooking(revealContent: any): void {
    for (let i = 0; i < this.chosenApartments.length; i++) {
      this.apartmentsIds.push(this.chosenApartments[i].id);
    }

    let booking = {
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
      guestsFullNames: this.guestsData,
      firstName: this.firstName,
      lastName: this.lastName,
      nights: this.diffDays,
      ApartmentsIds: this.apartmentsIds,
    };

    fetch(`https://apartmain.azurewebsites.net/api/staybookings/addstaybooking`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(booking),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.newStayBooking = response.resStayBooking;
          this.formLetter();
          this.sendInfoLetter();
          if (AuthHelper.isLogged()) {
            this.router.navigate(['/userbookings']);
          } else {
            this.showSuccessBooking(revealContent);
            this.router.navigate(['']);
          }
          BookingHelper.clearBookingData();
        } else {
          this.showAlert(response.message);
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  formLetter(): void {
    this.letterMessage = `
    Your booking has been successfully reserved! \n
    Booking details: \n
    Category: ${this.chosenSuggestion?.bookingCategory} \n
    Check-in: ${this.newStayBooking
      .checkIn!.toString()
      .substring(0, this.newStayBooking.checkIn!.toString().indexOf('T'))} \n
    Check-out: ${this.newStayBooking
      .checkOut!.toString()
      .substring(0, this.newStayBooking.checkOut!.toString().indexOf('T'))} \n
    Nights: ${this.newStayBooking.nights} \n
    Is for work: ${this.newStayBooking.isForWork} \n
    Special requests: ${this.newStayBooking.specialRequests} \n
    Total price: ${this.newStayBooking.price?.totalPrice} \n
    Used promocode: ${this.newStayBooking.promoCode} \n
    Discount with promo code: ${this.newStayBooking.price?.discount}% (-$${
      this.newStayBooking.price?.difference
    }) \n
    Final price: ${this.newStayBooking.price?.finalPrice} \n
    Customer: ${this.newStayBooking.customerInfo?.firstName} ${
      this.newStayBooking.customerInfo?.lastName
    } \n
    Customer email: ${this.newStayBooking.customerInfo?.email} \n
    Customer phone number: ${this.newStayBooking.customerInfo?.phoneNumber} \n
    Customer address: ${
      this.newStayBooking.customerInfo?.addressText +
      ', ' +
      this.newStayBooking.customerInfo?.city +
      ', ' +
      this.newStayBooking.customerInfo?.country +
      ' ' +
      this.newStayBooking.customerInfo?.zipCode
    } \n\n

    Guests: \n
    `;

    for (let i = 0; i < this.newStayBooking.stayBookingsGuests.length; i++) {
      this.letterMessage +=
        this.newStayBooking.stayBookingsGuests[i].guest.fullName;
      this.letterMessage += ', ';
    }

    this.letterMessage += `\n\n
    Your booking unique number: ${this.newStayBooking.uniqueNumber}. \n
    Your booking PIN: ${this.newStayBooking.pin}. \n
    Save it in order to you have an opportunity to have an access to your booking!
    `;
  }

  sendInfoLetter(): void {
    fetch(
      `https://apartmain.azurewebsites.net/api/notifications/sendsuccessbookingmail?email=${this.email}&message=${this.letterMessage}`,
      {
        method: 'GET',
      }
    )
      .then((r) => r.json())
      .then(async (data) => {
        if (data.code === 200) {
        } else {
          this.showAlert(data.message);
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  showSuccessBooking(revealContent: any): void {
    this.openVerticallyCentered(revealContent);
  }

  showSuggestion(uniqueCode: number): void {
    this.router.navigate(['suggestion', uniqueCode], {
      queryParams: {
        isSaved: this.isSaved,
      },
    });
  }

  ngOnInit(): void {
    if (BookingHelper.getBookingData()) {
      this.chosenApartments = this.bookingDetailsService.getChosenApartments();
      this.chosenSuggestion = this.bookingDetailsService.getChosenSuggestion();
      this.grade = this.bookingDetailsService.getGrade();
      this.diffDays = this.bookingDetailsService.getDiffDays();
      this.checkIn = this.bookingDetailsService.getCheckInDate();
      this.checkOut = this.bookingDetailsService.getCheckOutDate();
      this.guestsData = this.bookingDetailsService.getGuestsData();

      let bookingData = BookingHelper.getBookingData();
      this.chosenApartments = bookingData.chosenApartments as any;
      this.chosenSuggestion = bookingData.chosenSuggestion;
      this.grade = +bookingData.bookingGrade;
      this.diffDays = +bookingData.bookingDiffDays;
      this.checkIn = bookingData.checkIn;
      this.checkOut = bookingData.checkOut;
      this.guestsData = BookingHelper.getGuestsData() as any;

      this.activatedRouter.queryParams.subscribe((params: any) => {
        if (params['totalPrice']) {
          this.totalPrice = params['totalPrice'];
          this.finalPrice = this.totalPrice;
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
        if (params['specialRequests']) {
          this.specialRequests = params['specialRequests'];
        }
      });
    } else {
      this.router.navigate(['']);
    }
  }
}
