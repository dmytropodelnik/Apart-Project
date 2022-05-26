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
      'https://apartmain.azurewebsites.net/api/codes/generatesubscriptioncode?email=' +
        this.email,
      {
        method: 'GET',
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          alert('We sent verification letter to your email!');
          console.log(data);
        }
        else {
          alert(data.message);
        }
        this.email = '';
      })
      .catch((ex) => {
        alert(ex);
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
