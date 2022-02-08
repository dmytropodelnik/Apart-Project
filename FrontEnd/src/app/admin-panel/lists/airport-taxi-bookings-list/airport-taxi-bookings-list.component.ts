import { Component, OnInit } from '@angular/core';
import { AirportTaxiBooking } from 'src/app/models/Services/airporttaxibooking.item';

@Component({
  selector: 'app-airport-taxi-bookings-list',
  templateUrl: './airport-taxi-bookings-list.component.html',
  styleUrls: ['./airport-taxi-bookings-list.component.css']
})
export class AirportTaxiBookingsListComponent implements OnInit {

  bookings: AirportTaxiBooking[] | null = null;

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
