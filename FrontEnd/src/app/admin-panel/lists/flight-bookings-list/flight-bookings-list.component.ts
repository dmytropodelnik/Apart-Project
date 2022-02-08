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
    fetch('https://localhost:44381/api/countries/getcountries', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.bookings = data.bookings;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

}
