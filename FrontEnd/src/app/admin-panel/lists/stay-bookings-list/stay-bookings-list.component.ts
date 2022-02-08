import { Component, OnInit } from '@angular/core';
import { StayBooking } from 'src/app/models/Services/staybooking.item';

@Component({
  selector: 'app-stay-bookings-list',
  templateUrl: './stay-bookings-list.component.html',
  styleUrls: ['./stay-bookings-list.component.css']
})
export class StayBookingsListComponent implements OnInit {

  bookings: StayBooking[] | null = null;

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
