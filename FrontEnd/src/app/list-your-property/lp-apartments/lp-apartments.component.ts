import { Component, OnInit } from '@angular/core';

import { ActivatedRoute, Router } from '@angular/router';
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
    private router: Router,
    private activatedRouter: ActivatedRoute,
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
      alert("Fill all fields!")
      return;
    }

    for (let i = 0; i < this.apartments.length; i++) {
      if (this.apartments[i].name.length < 3) {
        alert("Apartment name must have at least 3 characters!");
        return;
      } else if (this.apartments[i].description.length < 10) {
        alert("Apartment description must have at least 10 characters!");
        return;
      } else if (!this.apartments[i].priceInUSD?.toString().match(/\d+/)) {
        alert("Apartment price must have at least 1 character!");
        return;
      } else if (!this.apartments[i].roomsAmount?.toString().match(/\d+/)) {
        alert("Apartment rooms amount must have at least 1 character!");
        return;
      } else if (!this.apartments[i].guestsLimit?.toString().match(/\d+/)) {
        alert("Apartment guests limit must have at least 1 character!");
        return;
      } else if (!this.apartments[i].bathroomsAmount?.toString().match(/\d+/)) {
        alert("Apartment bathrooms amount limit must have at least 1 character!");
        return;
      }
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
    this.activatedRouter.queryParams.subscribe((params: any) => {
      if (params['toSaveId'] == 'true') {
        this.listNewPropertyService.setSavedPropertyId(
          params['id']
        );
      }
      if (!AuthHelper.isLogged()) {
        this.router.navigate(['']);
      }
      else if (!this.listNewPropertyService.getSavedPropertyId()) {
        this.router.navigate(['']);
      }
    });
  }

}
