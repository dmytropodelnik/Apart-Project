import { Component, OnInit } from '@angular/core';
import { Favorite } from '../models/UserData/favorite.item';

import AuthHelper from '../utils/authHelper';
import ImageHelper from '../utils/imageHelper';
import MathHelper from '../utils/mathHelper';

import { AuthorizationService } from '../services/authorization.service';
import { Router } from '@angular/router';
import { MainDataService } from '../services/main-data.service';

@Component({
  selector: 'app-user-saved',
  templateUrl: './user-saved.component.html',
  styleUrls: ['./user-saved.component.css'],
})
export class UserSavedComponent implements OnInit {
  favorites: Favorite | null = null;

  imageHelper: any = ImageHelper;
  mathHelper: any = MathHelper;

  suggestionGrades: any;
  reviewsCount: any;

  userId: number | null = null;

  constructor(
    public mainDataService: MainDataService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getUserFavorites();
  }

  removeSuggestion(id: any): void {
    const suggestion = {
      id: id,
      login: AuthHelper.getLogin(),
    };

    fetch(
      'https://localhost:44381/api/favorites/removesuggestion', {
        method: 'DELETE',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: 'Bearer ' + AuthHelper.getToken(),
        },
        body: JSON.stringify(suggestion),
      }
    )
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {

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
          this.suggestionGrades = response.suggestionGrades;
          this.reviewsCount = response.reviewsCount;
        } else {
          alert('User favorites fetching error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }
}
