import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Favorite } from '../models/UserData/favorite.item';

import AuthHelper from '../utils/authHelper';
import ImageHelper from '../utils/imageHelper';
import MathHelper from '../utils/mathHelper';

import { AuthorizationService } from '../services/authorization.service';
import { Router } from '@angular/router';
import { MainDataService } from '../services/main-data.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-user-saved',
  templateUrl: './user-saved.component.html',
  styleUrls: ['./user-saved.component.css'],
})
export class UserSavedComponent implements OnInit {
  favorites: Favorite = new Favorite();

  imageHelper: any = ImageHelper;
  mathHelper: any = MathHelper;

  suggestionGrades: any;
  reviewsCount: any;

  suggestions: any[] = [];

  suggestionStartsFrom: any[] = [];

  userId: number | null = null;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private authService: AuthorizationService,
    public mainDataService: MainDataService,
    private router: Router,
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {
    this.getUserFavorites();
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
          this.suggestions = this.suggestions.filter((s) => {
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
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  showSuggestion(uniqueCode: number): void {
    this.router.navigate(['suggestion', uniqueCode], {
      queryParams: {
        isSaved: true,
      },
    });
  }

  getUserFavorites(): void {
    let url =
      'https://localhost:44381/api/favorites/getuserfavorites?email=' +
      AuthHelper.getLogin();
    if (AuthHelper.isFacebookLogin()) {
      url += '&isFacebookAuth=true';
    }

    fetch(url, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.suggestions = response.favorites;
          this.suggestionGrades = response.suggestionGrades;
          this.reviewsCount = response.reviewsCount;
          this.suggestionStartsFrom = response.suggestionStartsFrom;
        } else {
          alert('User favorites fetching error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }
}
