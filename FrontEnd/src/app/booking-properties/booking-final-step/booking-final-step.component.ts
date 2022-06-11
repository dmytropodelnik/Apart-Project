import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-booking-final-step',
  templateUrl: './booking-final-step.component.html',
  styleUrls: ['./booking-final-step.component.css']
})
export class BookingFinalStepComponent implements OnInit {


  finalPrice: number | null = null;

  applyPromoCode(): void {

  }

  constructor() { }

  ngOnInit(): void {

  }

}
