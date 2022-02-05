import { Component, OnInit } from '@angular/core';
import { BookingCategory } from '../models/bookingcategory.item';
import { City } from '../models/Location/city.item';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  card = [
    {card: 1},{card: 1},{card: 1},{card: 1},{card: 1},
  ]

  displayMonths = 2;
  navigation = 'arrows';
  showWeekNumbers = false;
  outsideDays = 'hidden';

  bookingCategories: BookingCategory[] | undefined;
  cities: City[] | undefined;

  slides = [
    {text:"Educational Consulting", img:"assets/images/21.png"},
    {text:"University and Higher Education", img:"assets/images/21.png"},
    {text:"Migration Consulting", img:"assets/images/21.png"},
    {text:"NAATI Translations", img:"assets/images/21.png"},
    {text:"English Courses", img:"assets/images/21.png"},
    {text:"Tax Return", img:"assets/images/21.png"},
    {text:"Tax Return", img:"assets/images/21.png"},
    {text:"Tax Return", img:"assets/images/21.png"},
    {text:"Tax Return", img:"assets/images/21.png"},
    {text:"Tax Return", img:"assets/images/21.png"},
    {text:"Tax Return", img:"assets/images/21.png"},
  ];

  slideConfig = {
    "slidesToShow": 6,
    "slidesToScroll": 1,
    "dots": false,
    "infinite": true,
    "arrows": true,
    "autoplay": true,
    "responsive": [
      {
        breakpoint: 1024,
        settings: {
          slidesToShow: 3,
          slidesToScroll: 1,

        }
      },
      {
        breakpoint: 600,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
          arrows: true,


        }
      },
    ]
  };

  slideConfig1 = {
    "slidesToShow": 4,
    "slidesToScroll": 1,
    "dots": false,
    "infinite": true,
    "arrows": true,
    "autoplay": true,
    "responsive": [
      {
        breakpoint: 1024,
        settings: {
          slidesToShow: 3,
          slidesToScroll: 1,

        }
      },
      {
        breakpoint: 600,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
          arrows: true,


        }
      },
    ]
  };



  constructor() { }

  ngOnInit(): void {
    fetch(
      'https://localhost:44381/api/bookingcategories/getcategories', {
        method: 'GET',
      }
    )
      .then((r) => r.json())
      .then((r) => {
        if (r.code === 200) {
          this.bookingCategories = r.bookingCategories;
        } else {
          alert('Booking categories fetching error!');
        }
      })
      .catch((err) => {
        alert(err);
      });

      fetch(
        'https://localhost:44381/api/cities/getcountrycities?country=Ukraine', {
          method: 'GET',
        }
      )
        .then((r) => r.json())
        .then((r) => {
          if (r.code === 200) {
            this.cities = r.cities;
          } else {
            alert('Cities fetching error!');
          }
        })
        .catch((err) => {
          alert(err);
        });
  }

}
