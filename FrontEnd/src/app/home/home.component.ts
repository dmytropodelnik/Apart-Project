import { Component, OnInit, OnDestroy } from '@angular/core';
import { BookingCategory } from '../models/bookingcategory.item';
import { City } from '../models/Location/city.item';
import { Suggestion } from '../models/Suggestions/suggestion.item';

import { Router } from '@angular/router';

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
  imagePath: string = '123';
  cities: City[] | undefined;
  citySuggestions: any;
  suggestions: any;
  footerCities: any;
  placesOfInterestSuggestions: any;
  placesOfInterests: any;
  regionsSuggestions: any;
  regions: any;

  recommendedSuggestionsCount: any;
  recommendedCities: any;

  homeGuestsSuggestions: any;
  resSuggestion: any;
  reviewsCount: any;
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
    public mainDataService: MainDataService,
    private router: Router,
    ) {

    }

  getRecommendedDestData(): void {
    fetch(`https://localhost:44381/api/stayspage/getrecommendeddestdata`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.recommendedSuggestionsCount = data.suggestionsCount;
          this.recommendedCities = data.citiesList;
        } else {
          alert("Recommended dest data fetching error!");
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getCategoriesData(): void {
    fetch('https://localhost:44381/api/stayspage/getcategoriesdata?country=' + this.mainDataService.getCurrentCountry(), {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((r) => {
        if (r.code === 200) {
          this.suggestions = r.suggestions;
          this.bookingCategories = r.categories;
        } else {
          alert('Categories data fetching error!');
        }
      })
      .catch((err) => {
        alert(err);
      });
  }

  getRegionsData(): void {
    fetch('https://localhost:44381/api/stayspage/getregionsdata', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((r) => {
        if (r.code === 200) {
          this.regions = r.regions;
          this.regionsSuggestions = r.regionsSuggestions;
        } else {
          alert('Data fetching error!');
        }
      })
      .catch((err) => {
        alert(err);
      });
  }

  getInterestPlacesData(): void {
    fetch('https://localhost:44381/api/stayspage/getinterestplacesdata', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((r) => {
        if (r.code === 200) {
          this.placesOfInterestSuggestions = r.placesOfInterestSuggestions;
          this.placesOfInterests = r.placesOfInterests;
        } else {
          alert('Data fetching error!');
        }
      })
      .catch((err) => {
        alert(err);
      });
  }

  getCitiesData(): void {
    fetch('https://localhost:44381/api/stayspage/getcitiesdata?country=' + this.mainDataService.getCurrentCountry(), {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((r) => {
        if (r.code === 200) {
          this.cities = r.cities;
          this.citySuggestions = r.citySuggestions;
          this.footerCities = r.footerCities;
        } else {
          alert('Data fetching error!');
        }
      })
      .catch((err) => {
        alert(err);
      });
  }

  getGuestsLoveData(): void {
    fetch(`https://localhost:44381/api/stayspage/getguestslovedata`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.homeGuestsSuggestions = data.resSuggestion;
          this.reviewsCount = data.reviewsCount;
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  searchSuggestions(): void {
    fetch(`https://localhost:44381/api/stayspage/search`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.homeGuestsSuggestions = data.resSuggestion;
          this.reviewsCount = data.reviewsCount;

          // this.router.navigate(['']);
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  ngOnInit(): void {
    this.getCategoriesData();
    this.getRegionsData();
    this.getInterestPlacesData();
    this.getCitiesData();
    this.getRecommendedDestData();
    this.getGuestsLoveData();
  }

  ngOnDestroy() {}
}
