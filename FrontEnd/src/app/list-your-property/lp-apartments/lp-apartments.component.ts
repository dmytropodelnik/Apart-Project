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

  apartments: Apartment[] = [];
  apartmentsAmount: boolean[] = [true];

  constructor(
    private listNewPropertyService: ListNewPropertyService,
    private router: Router
  ) {

  }

  addApartmentSetting(): void {
    this.apartmentsAmount.push(true);
  }

  removeApartmentSetting(): void {
    this.apartmentsAmount.pop();
  }

  addApartments(): void {
    let suggestion = {
      id: this.listNewPropertyService.getSavedPropertyId(),
      login: AuthHelper.getLogin(),
      apartments: this.apartments,
    };

    fetch(`https://localhost:44381/api/listnewproperty/addapartments`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
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
