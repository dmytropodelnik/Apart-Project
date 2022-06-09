import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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
    isSuite: string;
    isSmokingAllowed: boolean;
  }[] = [];

  grade: number = 0;
  diffDays: number = 0;

  checkIn: Date | null = null;
  checkOut: Date | null = null;

  mathHelper: any = MathHelper;

  constructor(
    private router: Router,
    private activatedRouter: ActivatedRoute,
    private bookingDetailsService: BookingDetailsService,
    ) {

    }

  ngOnInit(): void {
    this.chosenApartments = this.bookingDetailsService.getChosenApartments();
    this.chosenSuggestion = this.bookingDetailsService.getChosenSuggestion();
    this.grade = this.bookingDetailsService.getGrade();
    this.diffDays = this.bookingDetailsService.getDiffDays();
    this.checkIn = this.bookingDetailsService.getCheckInDate();
    this.checkOut = this.bookingDetailsService.getCheckOutDate();
    // if (this.bookingDetailsService.getChosenApartments() != null) {
    //   this.chosenApartments = this.bookingDetailsService.getChosenApartments();
    //   this.chosenSuggestion = this.bookingDetailsService.getChosenSuggestion();
    //   console.log(this.chosenSuggestion);
    // } else {
    //   this.router.navigate(['']);
    // }
  }

}
