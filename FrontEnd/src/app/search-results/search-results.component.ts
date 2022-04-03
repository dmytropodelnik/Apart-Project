import { Component, OnInit } from '@angular/core';

import { Router } from '@angular/router';
import { ActivatedRoute} from '@angular/router';

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
  favorites: Favorite = new Favorite();

  // sorting
  sortState: any = SortState;
  sortOrder: any = SortState;

  // suggestions content
  resSuggestions: Suggestion[] = [];

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

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
  ) {

  }

  model: any;
  model1: any;

  addFilterCheck(filter: string, value: any, index: number): void {
    console.log(index);
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

    fetch(`https://localhost:44381/api/stayssearching/filtersearch`, {
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
          this.resSuggestions = data.suggestions;
          this.totalPages = Math.ceil(data.suggestionsAmount / 25);
          this.suggestionsAmount = data.suggestionsAmount;
        } else {
          alert('Suggestions sort fetching error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  searchItems(value: SortState = this.sortState.TopReviewed): void {
    this.filters.sortOrder = value;
    this.filters.pageSize = 25;
    this.filters.guestsAmount = this.filters.searchAdultsAmount + this.filters.searchChildrenAmount;

    fetch(`https://localhost:44381/api/stayssearching/search`, {
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
          this.resSuggestions = data.suggestions;
          this.totalPages = Math.ceil(data.suggestionsAmount / 25);
          this.suggestionsAmount = data.suggestionsAmount;
        } else {
          alert('Suggestions search fetching error!');
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
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(suggestion),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.favorites.suggestions.push(response.resSuggestion);
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
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(suggestion),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.favorites.suggestions = this.favorites.suggestions.filter((s) => {
            if (s.id === response.resSuggestion.id ) {
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
      }
    )
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.favorites = response.favorites;
        } else {
          alert('User favorites fetching error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  isSaved(id: number | null): boolean {
    for (let i = 0; i < this.favorites.suggestions.length; i++) {
      if (this.favorites.suggestions[i].id == id) {
        return true;
      }
    }
    return false;
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

    // Note: Below 'queryParams' can be replaced with 'params' depending on your requirements
    this.activatedRoute.queryParams.subscribe((params: any) => {
      this.filters.place = params['place'];
      this.filters.dateIn = params['dateIn'];
      this.filters.dateOut = params['dateOut'];
      this.filters.searchAdultsAmount = params['adults'];
      this.filters.searchChildrenAmount = params['children'];
      this.filters.searchRoomsAmount = params['rooms'];
    });

    console.log(this.filters.dateIn?.year);
    console.log(this.filters.dateOut?.month);

    this.searchItems();
  }
}
