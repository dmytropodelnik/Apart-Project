import { Component, OnInit } from '@angular/core';
import { FlightBooking } from 'src/app/models/Services/flightbooking.item';

@Component({
  selector: 'app-flight-bookings-list',
  templateUrl: './flight-bookings-list.component.html',
  styleUrls: ['./flight-bookings-list.component.css']
})
export class FlightBookingsListComponent implements OnInit {

  bookings: FlightBooking[] | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}
