import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';

import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Apartment } from 'src/app/models/Suggestions/apartment.item';
import { MainDataService } from 'src/app/services/main-data.service';

import { ListNewPropertyService } from '../../services/list-new-property.service';

import AuthHelper from '../../utils/authHelper';

@Component({
  selector: 'app-lp-apartments',
  templateUrl: './lp-apartments.component.html',
  styleUrls: ['./lp-apartments.component.css'],
})
export class LpApartmentsComponent implements OnInit {
  apartments: Apartment[] = [new Apartment()];
  apartmentsAmount: boolean[] = [true];

  validationErrors: boolean[] = [];

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private listNewPropertyService: ListNewPropertyService,
    private router: Router,
    private activatedRouter: ActivatedRoute,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  addApartmentSetting(): void {
    this.apartmentsAmount.push(true);
    this.apartments.push(new Apartment());
  }

  removeApartmentSetting(): void {
    if (this.apartmentsAmount.length > 1) {
      this.apartmentsAmount.pop();
    }
  }

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  addApartments(): void {
    if (this.validationErrors.length > 0) {
      this.showAlert('Fill all fields!');
      return;
    }

    for (let i = 0; i < this.apartments.length; i++) {
      if (this.apartments[i].name.length < 3) {
        this.showAlert('Apartment name must have at least 3 characters!');
        return;
      } else if (this.apartments[i].description.length < 10) {
        this.showAlert(
          'Apartment description must have at least 10 characters!'
        );
        return;
      } else if (!this.apartments[i].priceInUSD?.toString().match(/\d+/)) {
        this.showAlert('Apartment price must have at least 1 character!');
        return;
      } else if (!this.apartments[i].roomsAmount?.toString().match(/\d+/)) {
        this.showAlert(
          'Apartment rooms amount must have at least 1 character!'
        );
        return;
      } else if (!this.apartments[i].guestsLimit?.toString().match(/\d+/)) {
        this.showAlert(
          'Apartment guests limit must have at least 1 character!'
        );
        return;
      } else if (!this.apartments[i].bathroomsAmount?.toString().match(/\d+/)) {
        this.showAlert(
          'Apartment bathrooms amount limit must have at least 1 character!'
        );
        return;
      }
    }

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
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  ngOnInit(): void {
    this.activatedRouter.queryParams.subscribe((params: any) => {
      if (params['toSaveId'] == 'true') {
        this.listNewPropertyService.setSavedPropertyId(params['id']);
      }
      if (!AuthHelper.isLogged()) {
        this.router.navigate(['']);
      } else if (!this.listNewPropertyService.getSavedPropertyId()) {
        this.router.navigate(['']);
      }
    });
  }
}
