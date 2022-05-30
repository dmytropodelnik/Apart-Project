import { Component, OnInit } from '@angular/core';
import { Suggestion } from '../models/Suggestions/suggestion.item';

import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';

import AuthHelper from '../utils/authHelper';
import ImageHelper from '../utils/imageHelper';
import MathHelper from '../utils/mathHelper';

@Component({
  selector: 'app-view-property',
  templateUrl: './view-properties.component.html',
  styleUrls: ['./view-properties.component.css'],
})
export class ViewPropertyComponent implements OnInit {
  imageHelper: any = ImageHelper;

  inProgressSuggestions: any[] = [];
  activeSuggestions: any[] = [];

  activeSuggestionsAmount: number = 0;
  inProgressSuggestionsAmount: number = 0;

  constructor(private router: Router, private activatedRoute: ActivatedRoute) {}

  getProperties(): void {
    fetch(
      'https://localhost:44381/api/suggestions/getusersuggestions?email=' +
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
          this.inProgressSuggestions = response.inProgressSuggestions;
          this.activeSuggestions = response.activeSuggestions;
          this.activeSuggestionsAmount = response.activeSuggestionsAmount;
          this.inProgressSuggestionsAmount =
            response.inProgressSuggestionsAmount;
        } else {
          alert('User favorites fetching error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  showSuggestion(uniqueCode: number, id: number): void {
    this.router.navigate(['suggestion', uniqueCode], {
      queryParams: {},
    });
  }

  editSuggestion(uniqueCode: number, id: number): void {
    this.router.navigate(['suggestion', uniqueCode], {
      queryParams: {},
    });
  }

  ngOnInit(): void {
    this.getProperties();
  }
}
