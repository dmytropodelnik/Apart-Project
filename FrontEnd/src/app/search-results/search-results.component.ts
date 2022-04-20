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

  bookingCategories: BookingCategory[] = [];
  facilities: Facility[] = [];
  highlights: SuggestionHighlight[] = [];
  roomTypes: RoomType[] = [];
  staffLanguages: Language[] = [];
  bedTypes: BedType[] = [];

  currentPage: number = 1;
  totalPages: number = 1;

  suggestionsAmount: number = 0;

  constructor(private router: Router, private activatedRoute: ActivatedRoute) {}

  model: any;
  model1: any;

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
    this.addMainSearchFilter();

    fetch(`https://apartmain.azurewebsites.net/api/stayssearching/filtersearch`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
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
    fetch('https://apartmain.azurewebsites.net/api/bookingcategories/getcategories', {
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
    fetch('https://apartmain.azurewebsites.net/api/facilities/getfacilities', {
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
    fetch('https://apartmain.azurewebsites.net/api/suggestionhighlights/gethighlights', {
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
    fetch('https://apartmain.azurewebsites.net/api/roomtypes/gettypes', {
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
    fetch('https://apartmain.azurewebsites.net/api/languages/getlanguages', {
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
    fetch('https://apartmain.azurewebsites.net/api/bedtypes/getbedtypes', {
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

    fetch('https://apartmain.azurewebsites.net/api/favorites/addsuggestion', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
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

    fetch('https://apartmain.azurewebsites.net/api/favorites/removesuggestion', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(suggestion),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.savedSuggestions = this.savedSuggestions.filter(
            (s) => {
              if (s.id === response.resSuggestion.id) {
                return false;
              } else {
                return true;
              }
            }
          );
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
      'https://apartmain.azurewebsites.net/api/favorites/getuserfavorites?email=' +
        AuthHelper.getLogin(),
      {
        method: 'GET',
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
      this.filters.searchAdultsAmount = params['adults'];
      this.filters.searchChildrenAmount = params['children'];
      this.filters.searchRoomsAmount = params['rooms'];

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
    });
  }

  ngOnInit(): void {
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
  }
}
