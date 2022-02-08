import { Component, OnInit } from '@angular/core';
import { CarRentalBooking } from 'src/app/models/Services/carrentalbooking.item';

@Component({
  selector: 'app-car-rental-bookings-list',
  templateUrl: './car-rental-bookings-list.component.html',
  styleUrls: ['./car-rental-bookings-list.component.css']
})
export class CarRentalBookingsListComponent implements OnInit {

  bookings: CarRentalBooking[] | null = null;

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
