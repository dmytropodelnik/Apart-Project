import { Component, OnInit, OnDestroy } from '@angular/core';
import { BookingCategory } from '../models/bookingcategory.item';
import { City } from '../models/Location/city.item';
import { Suggestion } from '../models/Suggestions/suggestion.item';

import { MainDataService } from '../services/main-data.service';

import ImageHelper from '../utils/imageHelper';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit, OnDestroy {
  card = [{ card: 1 }, { card: 1 }, { card: 1 }, { card: 1 }, { card: 1 }];

  displayMonths = 2;
  navigation = 'arrows';
  showWeekNumbers = false;
  outsideDays = 'hidden';
  imageHelper: any = ImageHelper;

  bookingCategories: BookingCategory[] | undefined;
  imagePath: string = "123";
  cities: City[] | undefined;
  citySuggestions: any;
  suggestions: any;
  footerCities: any;
  placesOfInterestSuggestions: any;
  placesOfInterests: any;
  regionsSuggestions: any;
  regions: any;

  slides = [
    { text: 'Educational Consulting', img: 'assets/images/21.png' },
    { text: 'University and Higher Education', img: 'assets/images/21.png' },
    { text: 'Migration Consulting', img: 'assets/images/21.png' },
    { text: 'NAATI Translations', img: 'assets/images/21.png' },
    { text: 'English Courses', img: 'assets/images/21.png' },
    { text: 'Tax Return', img: 'assets/images/21.png' },
    { text: 'Tax Return', img: 'assets/images/21.png' },
    { text: 'Tax Return', img: 'assets/images/21.png' },
    { text: 'Tax Return', img: 'assets/images/21.png' },
    { text: 'Tax Return', img: 'assets/images/21.png' },
    { text: 'Tax Return', img: 'assets/images/21.png' },
  ];

  slideConfig = {
    slidesToShow: 6,
    slidesToScroll: 1,
    dots: false,
    infinite: true,
    arrows: true,
    autoplay: true,
    responsive: [
      {
        breakpoint: 1024,
        settings: {
          slidesToShow: 3,
          slidesToScroll: 1,
        },
      },
      {
        breakpoint: 600,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
          arrows: true,
        },
      },
    ],
  };

  slideConfig1 = {
    slidesToShow: 4,
    slidesToScroll: 1,
    dots: false,
    infinite: true,
    arrows: true,
    autoplay: true,
    responsive: [
      {
        breakpoint: 1024,
        settings: {
          slidesToShow: 3,
          slidesToScroll: 1,
        },
      },
      {
        breakpoint: 600,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
          arrows: true,
        },
      },
    ],
  };

  constructor(
    public mainDataService: MainDataService
  ) {
  }

  ngOnInit(): void {
    fetch(
      'https://localhost:44381/api/stayspage/getdata?country=Ukraine',
      {
        method: 'GET',
      }
    )
      .then((r) => r.json())
      .then((r) => {
        if (r.code === 200) {
          this.cities = r.cities;
          this.citySuggestions = r.citySuggestions;
          this.suggestions = r.suggestions;
          this.bookingCategories = r.categories;
          this.footerCities = r.footerCities;
          this.placesOfInterestSuggestions = r.placesOfInterestSuggestions;
          this.placesOfInterests = r.placesOfInterests;
          this.regions = r.regions;
          this.regionsSuggestions = r.regionsSuggestions;
        } else {
          alert('Data fetching error!');
        }
        console.log(r.categories);
        console.log(r.cities);
        console.log(r.suggestions);
        console.log(r.citySuggestions);
        console.log(r.footerCities);
        console.log(r.placesOfInterestSuggestions);
        console.log(r.placesOfInterests);
        console.log(r.regions);
        console.log(r.regionsSuggestions);
      })
      .catch((err) => {
        alert(err);
      });
  }

  ngOnDestroy() {

  }
}
