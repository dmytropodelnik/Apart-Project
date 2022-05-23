import { Component, OnInit } from '@angular/core';

import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';

import { switchMap } from 'rxjs/operators';

import AuthHelper from '../utils/authHelper';
import ImageHelper from '../utils/imageHelper';
import MathHelper from '../utils/mathHelper';

import { SortState } from '../enums/sortstate.item';
import { Suggestion } from '../models/Suggestions/suggestion.item';
import { SearchViewModel } from '../view-models/searchviewmodel.item';
import { FilterViewModel } from '../view-models/filterviewmodel.item';
import { BookingCategory } from '../models/bookingcategory.item';
import { Facility } from '../models/facility.item';
import { SuggestionHighlight } from '../models/Suggestions/suggestionhighlight.item';
import { RoomType } from '../models/Suggestions/roomtype.item';
import { Language } from '../models/language.item';
import { BedType } from '../models/Suggestions/bedtype.item';
import { Favorite } from '../models/UserData/favorite.item';
import { Subscription } from 'rxjs';
import { NgbDate } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-search-results',
  templateUrl: './search-results.component.html',
  styleUrls: ['./search-results.component.css'],
})
export class SearchResultsComponent implements OnInit {
  mathHelper: any = MathHelper;
  imageHelper: any = ImageHelper;

  // saved
  // favorites: Favorite = new Favorite();
  savedSuggestions: any[] = [];

  // sorting
  sortState: any = SortState;
  sortOrder: any = SortState;

  // suggestions content
  resSuggestions: any[] = [];

  filters: SearchViewModel = new SearchViewModel();
  filterChecks: FilterViewModel[] = [];
  filterCheckBoxes: boolean[] = [];
  suggestionsAmountWithFilters: number[] = [];

  bookingCategories: BookingCategory[] = [];
  facilities: Facility[] = [];
  highlights: SuggestionHighlight[] = [];
  roomTypes: RoomType[] = [];
  staffLanguages: Language[] = [];
  bedTypes: BedType[] = [];

  suggestionStartsFrom: any[] = [];
  suggestionGrades: any;

  currentPage: number = 1;
  totalPages: number = 1;

  searchBookingCategory: string = '';
  searchPlace: string = '';

  suggestionsAmount: number = 0;

  yearIn: number | null = null;
  monthIn: number | null = null;
  dayIn: number | null = null;

  yearOut: number | null = null;
  monthOut: number | null = null;
  dayOut: number | null = null;

  constructor(private router: Router, private activatedRoute: ActivatedRoute) {
    this.suggestionsAmountWithFilters.fill(0);
  }

  model: any;
  model1: any;

  setChildren(value: number): void {
    if (this.filters.searchChildrenAmount + value < 0) {
      return;
    }
    this.filters.searchChildrenAmount =
      +this.filters.searchChildrenAmount + +value;
  }

  setAdults(value: number): void {
    if (this.filters.searchAdultsAmount + value < 1) {
      return;
    }
    this.filters.searchAdultsAmount = +this.filters.searchAdultsAmount + +value;
  }

  setRooms(value: number): void {
    if (this.filters.searchRoomsAmount + value < 1) {
      return;
    }
    this.filters.searchRoomsAmount = +this.filters.searchRoomsAmount + +value;
  }

  searchSuggestions($event: any): void {
    $event.stopPropagation();

    this.filters.sortOrder = SortState.TopReviewed;
    console.log(this.filters);

    let dateIn, dateOut;

    if (this.filters.pdateIn && this.filters.pdateOut) {
      dateIn =
        this.filters.pdateIn!.year +
        '-' +
        this.filters.pdateIn!.month +
        '-' +
        this.filters.pdateIn!.day;
      dateOut =
        this.filters.pdateOut!.year +
        '-' +
        this.filters.pdateOut!.month +
        '-' +
        this.filters.pdateOut!.day;
    }

    this.filterChecks.shift();
    this.filterChecks.shift();
    this.filterChecks.shift();

    this.filterChecks.push(new FilterViewModel('places', this.filters.place));
    if (
      typeof this.filters.dateIn !== 'undefined' ||
      typeof this.filters.dateOut !== 'undefined'
    ) {
      this.filterChecks.push(
        new FilterViewModel(
          'dates',
          this.filters.dateIn + ';' + this.filters.dateOut
        )
      );
    }
    this.filterChecks.push(
      new FilterViewModel(
        'amounts',
        this.filters.searchAdultsAmount +
          ';' +
          this.filters.searchChildrenAmount +
          ';' +
          this.filters.searchRoomsAmount
      )
    );

    this.sortItems();
  }

  addFilterCheck(filter: string, value: any, index: any): void {
    if (this.filterCheckBoxes[index]) {
      this.filterChecks.push(new FilterViewModel(filter, value));
    } else {
      this.filterChecks = this.filterChecks.filter((f) => {
        if (f.value === value && f.filter === filter) {
          return false;
        } else {
          return true;
        }
      });
    }
    this.sortItems();
  }

  setCurrentPage(page: number): void {
    this.currentPage = page;
    this.filters.page = this.currentPage;
    this.sortItems();
  }

  sortItems(value: SortState = this.sortState.TopReviewed): void {
    this.filters.sortOrder = value;
    // this.filters.suggestions = this.resSuggestions;
    this.filters.pageSize = 25;
    this.filters.filters = this.filterChecks;
    this.filters.guestsAmount =
      Number(this.filters.searchAdultsAmount) +
      Number(this.filters.searchChildrenAmount);

    console.log(this.filters.filters);

    fetch(`https://localhost:44381/api/stayssearching/filtersearch`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(this.filters),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.resSuggestions = data.resSuggestions;
          this.totalPages =
            Math.ceil(data.suggestionsAmount / 25) == 0
              ? 1
              : Math.ceil(data.suggestionsAmount / 25);
          this.suggestionsAmount = data.suggestionsAmount;
          this.suggestionStartsFrom = data.suggestionStartsFrom;
          this.suggestionGrades = data.suggestionGrades;

          console.log(this.resSuggestions);
        } else {
          alert('Suggestions sort fetching error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getBookingCategories(): void {
    fetch('https://localhost:44381/api/bookingcategories/getcategories', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.bookingCategories = data.categories;
        } else {
          alert('Booking categories fetching error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getFacilities(): void {
    fetch('https://localhost:44381/api/facilities/getfacilities', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.facilities = data.facilities;
        } else {
          alert('Facilities fetching error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getHighlights(): void {
    fetch('https://localhost:44381/api/suggestionhighlights/gethighlights', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.highlights = data.highlights;
        } else {
          alert('Highlights fetching error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getTypes(): void {
    fetch('https://localhost:44381/api/roomtypes/gettypes', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.roomTypes = data.types;
        } else {
          alert('Room types fetching error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getLangs(): void {
    fetch('https://localhost:44381/api/languages/getlanguages', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.staffLanguages = data.languages;
        } else {
          alert('Staff languages fetching error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getBedTypes(): void {
    fetch('https://localhost:44381/api/bedtypes/getbedtypes', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.bedTypes = data.bedTypes;
        } else {
          alert('Bed types fetching error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addSuggestionToSaved(id: any): void {
    const suggestion = {
      id: id,
      login: AuthHelper.getLogin(),
    };

    fetch('https://localhost:44381/api/favorites/addsuggestion', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(suggestion),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.savedSuggestions.push(response.resSuggestion);
        } else {
          alert('User favorites fetching error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  removeSuggestion(id: any): void {
    const suggestion = {
      id: id,
      login: AuthHelper.getLogin(),
    };

    fetch('https://localhost:44381/api/favorites/removesuggestion', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(suggestion),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.savedSuggestions = this.savedSuggestions.filter((s) => {
            if (s.id === response.resSuggestion.id) {
              return false;
            } else {
              return true;
            }
          });
        } else {
          alert('User favorites fetching error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getUserFavorites(): void {
    fetch(
      'https://localhost:44381/api/favorites/getuserfavorites?email=' +
        AuthHelper.getLogin(),
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.savedSuggestions = response.favorites;
        } else {
          alert('User favorites fetching error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  isSaved(id: number | null): boolean {
    for (let i = 0; i < this.savedSuggestions.length; i++) {
      if (this.savedSuggestions[i].id == id) {
        return true;
      }
    }
    return false;
  }

  addMainSearchFilter(): void {
    // Note: Below 'queryParams' can be replaced with 'params' depending on your requirements
    this.activatedRoute.queryParams.subscribe((params: any) => {
      this.filters.place = params['place'];
      this.filters.dateIn = params['dateIn'];
      this.filters.dateOut = params['dateOut'];
      this.filters.pdateIn = new NgbDate(
        +params['yearIn'],
        +params['monthIn'],
        +params['dayIn']
      );
      this.filters.pdateOut = new NgbDate(
        +params['yearOut'],
        +params['monthOut'],
        +params['dayOut']
      );

      this.yearIn = +params['yearIn'];
      this.monthIn = +params['monthIn'];
      this.dayIn = +params['dayIn'];

      this.yearOut = +params['yearOut'];
      this.monthOut = +params['monthOut'];
      this.dayOut = +params['dayOut'];

      this.filters.searchAdultsAmount = params['adults'];
      this.filters.searchChildrenAmount = params['children'];
      this.filters.searchRoomsAmount = params['rooms'];

      this.searchBookingCategory = params['bookingCategory'];

      if (this.filters.place) {
        this.filterChecks.push(
          new FilterViewModel('places', this.filters.place)
        );
        this.searchPlace = this.filters.place;
      }
      if (
        typeof this.filters.dateIn !== 'undefined' ||
        typeof this.filters.dateOut !== 'undefined'
      ) {
        this.filterChecks.push(
          new FilterViewModel(
            'dates',
            this.filters.dateIn + ';' + this.filters.dateOut
          )
        );
      }
      this.filterChecks.push(
        new FilterViewModel(
          'amounts',
          this.filters.searchAdultsAmount +
            ';' +
            this.filters.searchChildrenAmount +
            ';' +
            this.filters.searchRoomsAmount
        )
      );
      if (this.searchBookingCategory) {
        this.filterChecks.push(
          new FilterViewModel('bookingCategories', this.searchBookingCategory)
        );
        this.searchPlace = this.searchBookingCategory;
      }
    });
  }

  showSuggestion(uniqueCode: number): void {
    this.router.navigate(['suggestion', uniqueCode], {
      queryParams: {
        place: this.filters.place,
        dateIn: this.filters.dateIn,
        dateOut: this.filters.dateOut,
        dayIn: this.dayIn,
        monthIn: this.monthIn,
        yearIn: this.yearIn,
        dayOut: this.dayOut,
        monthOut: this.monthOut,
        yearOut: this.yearOut,
        adults: this.filters.searchAdultsAmount,
        children: this.filters.searchChildrenAmount,
        rooms: this.filters.searchRoomsAmount,
      },
    });
  }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe((params: any) => {
      if (!params['adults'] || !params['rooms'] || !params['children']) {
        this.router.navigate(['']);
        return;
      }

      this.getBookingCategories();
      this.getFacilities();
      this.getHighlights();
      this.getTypes();
      this.getLangs();
      this.getBedTypes();
      if (AuthHelper.getLogin() != '') {
        this.getUserFavorites();
      }

      this.addMainSearchFilter();
      this.sortItems();
    });
  }
}
