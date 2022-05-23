import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { MainDataService } from '../services/main-data.service';

import AuthHelper from '../utils/authHelper';
import { SearchViewModel } from '../view-models/searchviewmodel.item';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css'],
})
export class FooterComponent implements OnInit {
  public isCollapsed = true;
  email: string = '';
  emailPattern = '^[a-z0-9._%+-]+@[a-z0-9.-]+.[a-z]{2,4}$';

  searchViewModel: SearchViewModel = new SearchViewModel();

  constructor(
    public mainDataService: MainDataService,
    private router: Router,
    ) {

  }

  addDealsSubscriber() {
    if (!this.email.match('^[a-z0-9._%+-]+@[a-z0-9.-]+.[a-z]{2,4}$')) {
      alert('Incorrect email pattern!');
      return;
    }

    fetch(
      'https://localhost:44381/api/deals/addsubscriber?email=' +
        this.email,
      {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((r) => r.json())
      .then((r) => {
        if (r.code === 200) {
          alert('You have been successfully subscribed to out new deals!');
        } else {
          alert('Add deals subscriber error!');
        }
        this.email = '';
      })
      .catch((err) => {
        alert(err);
      });
  }

  routerToMySettings(): void {
    if (AuthHelper.isLogged()) {
      this.router.navigate(['mysettings']);
    } else {
      this.router.navigate(['auth']);
    }
  }

  searchSuggestionsByCategory($event: any, category: string): void {
    $event.stopPropagation();

    this.router.navigate(['/searchresults'], {
      queryParams: {
        adults: this.searchViewModel.searchAdultsAmount,
        children: this.searchViewModel.searchChildrenAmount,
        rooms: this.searchViewModel.searchRoomsAmount,
        bookingCategory: category,
      },
    });
  }

  searchSuggestionsByPlace($event: any, place: string): void {
    $event.stopPropagation();

    this.router.navigate(['/searchresults'], {
      queryParams: {
        adults: this.searchViewModel.searchAdultsAmount,
        children: this.searchViewModel.searchChildrenAmount,
        rooms: this.searchViewModel.searchRoomsAmount,
        place: place,
      },
    });
  }

  ngOnInit(): void {}
}
