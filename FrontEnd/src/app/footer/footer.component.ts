import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

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

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    public mainDataService: MainDataService,
    private router: Router,
    private modalService: NgbModal
  ) {}

  addDealsSubscriber() {
    if (!this.email.match('^[a-z0-9._%+-]+@[a-z0-9.-]+.[a-z]{2,4}$')) {
      alert('Incorrect email pattern!');
      return;
    }

    fetch(
      'https://localhost:44381/api/codes/generatesubscriptioncode?email=' +
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
        } else {
          alert(data.message);
        }
        this.email = '';
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
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
