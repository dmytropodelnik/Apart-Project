import { Component, OnInit } from '@angular/core';

import { Router } from '@angular/router';
import { Apartment } from 'src/app/models/Suggestions/apartment.item';

import { ListNewPropertyService } from '../../services/list-new-property.service';

import AuthHelper from '../../utils/authHelper';

@Component({
  selector: 'app-lp-apartments',
  templateUrl: './lp-apartments.component.html',
  styleUrls: ['./lp-apartments.component.css']
})
export class LpApartmentsComponent implements OnInit {

  apartments: Apartment[] = [new Apartment()];
  apartmentsAmount: boolean[] = [true];

  validationErrors: boolean[] = [];

  constructor(
    private listNewPropertyService: ListNewPropertyService,
    private router: Router
  ) {

  }

  addApartmentSetting(): void {
    this.apartmentsAmount.push(true);
    this.apartments.push(new Apartment());
  }

  removeApartmentSetting(): void {
    if (this.apartmentsAmount.length > 1) {
      this.apartmentsAmount.pop();
    }
  }

  addApartments(): void {
    if (this.validationErrors.length > 0) {
      return;
    }

    let suggestion = {
      id: this.listNewPropertyService.getSavedPropertyId(),
      login: AuthHelper.getLogin(),
      apartments: this.apartments,
    };

    fetch(`https://apartmain.azurewebsites.net/api/listnewproperty/addapartments`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(suggestion),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200 && data.savedSuggestionId !== null) {
          this.router.navigate(['/lp/propertysetup']);
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  ngOnInit(): void {

  }

}
