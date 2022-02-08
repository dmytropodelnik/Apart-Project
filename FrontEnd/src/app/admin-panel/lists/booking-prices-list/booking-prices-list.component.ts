import { Component, OnInit } from '@angular/core';
import { BookingPrice } from 'src/app/models/Payment/bookingprice.item';

@Component({
  selector: 'app-booking-prices-list',
  templateUrl: './booking-prices-list.component.html',
  styleUrls: ['./booking-prices-list.component.css']
})
export class BookingPricesListComponent implements OnInit {

  bookings: BookingPrice[] | null = null;

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
