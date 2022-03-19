import { Component, OnInit } from '@angular/core';

import AuthHelper from '../utils/authHelper';

import { SortState } from '../enums/sortstate.item'
import { Suggestion } from '../models/Suggestions/suggestion.item';
import { SearchViewModel } from '../view-models/searchviewmodel.item';

@Component({
  selector: 'app-search-results',
  templateUrl: './search-results.component.html',
  styleUrls: ['./search-results.component.css'],
})

export class SearchResultsComponent implements OnInit {
  // sorting
  sortState: any = SortState;
  sortOrder: any = SortState;

  // suggestions content
  resSuggestions: Suggestion[] = [];

  filters: SearchViewModel = new SearchViewModel();

  constructor() {

  }

  model: any;
  model1: any;

  sortItems(value: SortState): void {
    this.filters.sortOrder = value;
    this.filters.suggestions = this.resSuggestions;
    this.filters.pageSize = 25;


    fetch(`https://localhost:44381/api/stayssearching/search`, {
      method: 'GET',
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
