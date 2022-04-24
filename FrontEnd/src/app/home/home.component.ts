import { Component, OnInit, OnDestroy } from '@angular/core';
import { BookingCategory } from '../models/bookingcategory.item';
import { City } from '../models/Location/city.item';
import { Suggestion } from '../models/Suggestions/suggestion.item';
import { SearchViewModel } from '../view-models/searchviewmodel.item';

import { Router } from '@angular/router';
import { ActivatedRoute} from '@angular/router';

import { MainDataService } from '../services/main-data.service';

import AuthHelper from '../utils/authHelper';
import ImageHelper from '../utils/imageHelper';
import MathHelper from '../utils/mathHelper';
import { SortState } from '../enums/sortstate.item';

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
  outsideDays = 'hidden';
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

  recommendedSuggestionsCount: any;
  recommendedCities: any;

  homeGuestsSuggestions: any;
  suggestionStartsFrom: any[] = [];
  resSuggestion: any;
  reviewsCount: any;
  suggestionGrades: any;

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
    private router: Router,
    ) {

    }

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

  getCountriesData(): void {
    fetch('https://localhost:44381/api/stayspage/getinterestplacesdata', {
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
    fetch('https://localhost:44381/api/stayspage/getcitiesdata?country=' + this.mainDataService.getCurrentCountry(), {
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
    fetch(`https://localhost:44381/api/stayspage/getguestslovedata`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.homeGuestsSuggestions = data.resSuggestion;
          this.suggestionStartsFrom = data.suggestionStartsFrom;
          this.reviewsCount = data.reviewsCount;
          this.suggestionGrades = data.suggestionGrades;

          console.log(this.suggestionStartsFrom);
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  searchSuggestions($event : any): void {
    $event.stopPropagation();

    this.searchViewModel.sortOrder = SortState.TopReviewed;
    console.log(this.searchViewModel);

    let dateIn, dateOut;

    if (this.searchViewModel.pdateIn && this.searchViewModel.pdateOut) {
      dateIn = this.searchViewModel.pdateIn!.day + '/' + this.searchViewModel.pdateIn!.month + '/' +
               this.searchViewModel.pdateIn!.year;
      dateOut = this.searchViewModel.pdateOut!.day + '/' + this.searchViewModel.pdateOut!.month + '/' +
                this.searchViewModel.pdateOut!.year;
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
      }
    });
  }

  ngOnInit(): void {
    this.getCategoriesData();
    this.getRegionsData();
    this.getCountriesData();
    this.getCitiesData();
    this.getRecommendedDestData();
    this.getGuestsLoveData();
  }

  ngOnDestroy() {}
}
