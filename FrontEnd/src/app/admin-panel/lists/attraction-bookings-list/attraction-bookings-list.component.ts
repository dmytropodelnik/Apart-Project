import { Component, OnInit } from '@angular/core';
import { AttractionBooking } from 'src/app/models/Services/attractionbooking.item';

@Component({
  selector: 'app-attraction-bookings-list',
  templateUrl: './attraction-bookings-list.component.html',
  styleUrls: ['./attraction-bookings-list.component.css']
})
export class AttractionBookingsListComponent implements OnInit {

  bookings: AttractionBooking[] | null = null;

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
