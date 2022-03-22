import { Component, OnInit } from '@angular/core';

import AuthHelper from '../utils/authHelper';
import MathHelper from '../utils/mathHelper';

import { SortState } from '../enums/sortstate.item'
import { Suggestion } from '../models/Suggestions/suggestion.item';
import { SearchViewModel } from '../view-models/searchviewmodel.item';
import { FilterViewModel } from '../view-models/filterviewmodel.item';
import { BookingCategory } from '../models/bookingcategory.item';
import { Facility } from '../models/facility.item';
import { SuggestionHighlight } from '../models/Suggestions/suggestionhighlight.item';
import { RoomType } from '../models/Suggestions/roomtype.item';
import { Language } from '../models/language.item';
import { BedType } from '../models/Suggestions/bedtype.item';

@Component({
  selector: 'app-search-results',
  templateUrl: './search-results.component.html',
  styleUrls: ['./search-results.component.css'],
})

export class SearchResultsComponent implements OnInit {
  mathHelper: any = MathHelper;

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

  constructor() {

  }

  model: any;
  model1: any;

  addFilterCheck(filter: string, value: any, index: number): void {
    console.log(index);
      if (this.filterCheckBoxes[index]) {
        this.filterChecks.push(new FilterViewModel(filter, value));
      } else {
        this.filterChecks = this.filterChecks.filter(f => {
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
          console.log(this.resSuggestions);
        } else {
          alert("Suggestions sort fetching error!");
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

  ngOnInit(): void {
    this.getBookingCategories();
    this.getFacilities();
    this.getHighlights();
    this.getTypes();
    this.getLangs();
    this.getBedTypes();
  }
}
