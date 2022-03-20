import { Component, OnInit } from '@angular/core';

import AuthHelper from '../utils/authHelper';
import MathHelper from '../utils/mathHelper';

import { SortState } from '../enums/sortstate.item'
import { Suggestion } from '../models/Suggestions/suggestion.item';
import { SearchViewModel } from '../view-models/searchviewmodel.item';
import { FilterViewModel } from '../view-models/filterviewmodel.item';

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

  constructor() {

  }

  model: any;
  model1: any;

  addFilterCheck(filter: string, value: number): void {
      if (this.filterCheckBoxes[value]) {
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
    this.filters.page = page;
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

  ngOnInit(): void {

  }
}
