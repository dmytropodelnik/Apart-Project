import { Component, OnInit, OnDestroy } from '@angular/core';
import { BookingCategory } from '../models/bookingcategory.item';
import { City } from '../models/Location/city.item';
import { Suggestion } from '../models/Suggestions/suggestion.item';
import { SearchViewModel } from '../view-models/searchviewmodel.item';

import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';

import { MainDataService } from '../services/main-data.service';

import AuthHelper from '../utils/authHelper';
import ImageHelper from '../utils/imageHelper';
import MathHelper from '../utils/mathHelper';
import { SortState } from '../enums/sortstate.item';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbDate } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit, OnDestroy {
  card = [{ card: 1 }, { card: 1 }, { card: 1 }, { card: 1 }, { card: 1 }];

  displayMonths = 1;
  navigation = 'arrows';
  showWeekNumbers = false;
  outsideDays = 'collapsed';
  current = new Date();
  minDate = {
    year: this.current.getFullYear(),
    month: this.current.getMonth() + 1,
    day: this.current.getDate(),
  };
  imageHelper: any = ImageHelper;
  mathHelper: any = MathHelper;

  citySuggestionsLength: number[] = [];

  bookingCategories: BookingCategory[] | undefined;
  imagePath: string = '123';
  cities: City[] | undefined;
  citySuggestions: any;
  suggestions: any;
  footerCities: string[] = [];
  countriesSuggestions: number[] = [];
  countries: string[] = [];
  regionsSuggestions: any;
  regions: any;
  reg = /[1-9\s\/\\!"#$%&'()*+,-.:;<=>?@[\]^_`{|}~]/g;
  reg1 = /[\/\\!"#$%&'()*+,-.:;<=>?@[\]^_`{|}~]/g;
  recommendedSuggestionsCount: any;
  recommendedCities: any;

  homeGuestsSuggestions: any;
  suggestionStartsFrom: any[] = [];
  resSuggestion: any;
  reviewsCount: any;
  suggestionGrades: any;

  isGotData: boolean = false;

  searchViewModel: SearchViewModel = new SearchViewModel();

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
    private router: Router
  ) {}

  setChildren(value: number): void {
    if (this.searchViewModel.searchChildrenAmount + value < 0) {
      return;
    }
    this.searchViewModel.searchChildrenAmount += value;
  }

  setAdults(value: number): void {
    if (this.searchViewModel.searchAdultsAmount + value < 1) {
      return;
    }
    this.searchViewModel.searchAdultsAmount += value;
  }

  setRooms(value: number): void {
    if (this.searchViewModel.searchRoomsAmount + value < 1) {
      return;
    }
    this.searchViewModel.searchRoomsAmount += value;
  }

  getRecommendedDestData(): void {
    fetch(`https://apartmain.azurewebsites.net/api/stayspage/getrecommendeddestdata`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.recommendedSuggestionsCount = data.suggestionsCount;
          this.recommendedCities = data.citiesList;
        } else {
          alert('Recommended dest data fetching error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getCategoriesData(): void {
    fetch('https://apartmain.azurewebsites.net/api/stayspage/getcategoriesdata?country=' + this.mainDataService.getCurrentCountry(), {
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
    fetch('https://apartmain.azurewebsites.net/api/stayspage/getregionsdata', {
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

  getCountriesData(): void {
    fetch('https://apartmain.azurewebsites.net/api/stayspage/getinterestplacesdata', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((r) => {
        if (r.code === 200) {
          this.countriesSuggestions = r.countriesSuggestions;
          this.countries = r.countries;
        } else {
          alert('Data fetching error!');
        }
      })
      .catch((err) => {
        alert(err);
      });
  }

  getCitiesData(): void {
    fetch('https://apartmain.azurewebsites.net/api/stayspage/getcitiesdata?country=' + this.mainDataService.getCurrentCountry(), {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((r) => {
        if (r.code === 200) {
          this.cities = r.cities;
          this.citySuggestionsLength = r.citySuggestionsLength;
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
    fetch(`https://apartmain.azurewebsites.net/api/stayspage/getguestslovedata`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.homeGuestsSuggestions = data.resSuggestion;
          this.suggestionStartsFrom = data.suggestionStartsFrom;
          this.reviewsCount = data.reviewsCount;
          this.suggestionGrades = data.suggestionGrades;
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  searchSuggestionsByCategory($event: any, category: string): void {
    $event.stopPropagation();

    this.router.navigate(['/searchresults'], {
      queryParams: {
        adults: this.searchViewModel.searchAdultsAmount,
        children: this.searchViewModel.searchChildrenAmount,
        rooms: this.searchViewModel.searchRoomsAmount,
        bookingCategory: category,
      },
    });
  }

  searchSuggestionsByPlace($event: any, place: string): void {
    $event.stopPropagation();

    this.router.navigate(['/searchresults'], {
      queryParams: {
        adults: this.searchViewModel.searchAdultsAmount,
        children: this.searchViewModel.searchChildrenAmount,
        rooms: this.searchViewModel.searchRoomsAmount,
        place: place,
      },
    });
  }

  searchSuggestions($event: any): void {
    $event.stopPropagation();

    this.searchViewModel.place = this.searchViewModel.place
      .replaceAll(this.reg1, '')
      .trim();

    let dateIn, dateOut;

    if (!this.searchViewModel.place) {
      return;
    }

    if (this.searchViewModel.pdateIn && this.searchViewModel.pdateOut) {
      dateIn =
        this.searchViewModel.pdateIn!.year +
        '-' +
        this.searchViewModel.pdateIn!.month +
        '-' +
        this.searchViewModel.pdateIn!.day;
      dateOut =
        this.searchViewModel.pdateOut!.year +
        '-' +
        this.searchViewModel.pdateOut!.month +
        '-' +
        this.searchViewModel.pdateOut!.day;
    } else {
      return;
    }

    this.router.navigate(['/searchresults'], {
      queryParams: {
        place: this.searchViewModel.place,
        dateIn: dateIn,
        dateOut: dateOut,
        dayIn: this.searchViewModel.pdateIn!.day,
        monthIn: this.searchViewModel.pdateIn!.month,
        yearIn: this.searchViewModel.pdateIn!.year,
        dayOut: this.searchViewModel.pdateOut!.day,
        monthOut: this.searchViewModel.pdateOut!.month,
        yearOut: this.searchViewModel.pdateOut!.year,
        adults: this.searchViewModel.searchAdultsAmount,
        children: this.searchViewModel.searchChildrenAmount,
        rooms: this.searchViewModel.searchRoomsAmount,
      },
    });
  }

  getData(): void {
    this.getCategoriesData();
    this.getRegionsData();
    this.getCountriesData();
    this.getCitiesData();
    this.getRecommendedDestData();
    this.getGuestsLoveData();
  }

  ngOnInit(): void {
    this.getData();
  }

  ngOnDestroy() {}
}
