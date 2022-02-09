import { Component, OnInit } from '@angular/core';
import { BookingCategory } from 'src/app/models/bookingcategory.item';

@Component({
  selector: 'app-booking-categories-list',
  templateUrl: './booking-categories-list.component.html',
  styleUrls: ['./booking-categories-list.component.css']
})
export class BookingCategoriesListComponent implements OnInit {
  categories: BookingCategory[] | null = null;

  constructor() { }

  ngOnInit(): void {
    fetch('https://localhost:44381/api/bookingcategories/getcategories', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.categories = data.categories;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

}
