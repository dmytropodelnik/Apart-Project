import { Component, OnInit } from '@angular/core';
import { Address } from 'src/app/models/Location/address.item';
import { Country } from 'src/app/models/Location/country.item';
import { ActivatedRoute, Router } from '@angular/router';

import { ListNewPropertyService } from '../../services/list-new-property.service';

import AuthHelper from '../../utils/authHelper';

@Component({
  selector: 'app-lp-name-and-location',
  templateUrl: './lp-name-and-location.component.html',
  styleUrls: ['./lp-name-and-location.component.css'],
})
export class LpNameAndLocationComponent implements OnInit {
  propertyName: string = '';
  propertyAddress: Address | null = null;
  uploadedFiles: File[] | null = null;

  sCountry: string = '';
  sCity: string = '';
  sZipCode: string = '';
  sAddress: string = '';
  sRegion: string = '';

  reg = /[!"#$%&'()*+,-.\/:;<=>?@[\]^_`{|}~]/gi;

  constructor(
    private listNewPropertyService: ListNewPropertyService,
    private router: Router,
    private activatedRouter: ActivatedRoute,
  ) {}
  choice: number = 0;

  incrementChoice() {
    let firstLine = document.getElementById('firstLine');
    if (firstLine !== null) {
      firstLine.classList.remove('navstep__container--active');
      firstLine.classList.add('navstep__container--after');
    }

    let secondLine = document.getElementById('secondLine');
    if (secondLine !== null) {
      secondLine.classList.add('navstep__container--active');
    }
    ++this.choice;
  }

  incrementChoice1() {
    let secondLine = document.getElementById('secondLine');
    if (secondLine !== null) {
      secondLine.classList.remove('navstep__container--active');
      secondLine.classList.add('navstep__container--after');
    }

    let thirdLine = document.getElementById('thirdLine');
    if (thirdLine !== null) {
      thirdLine.classList.add('navstep__container--active');
    }
    ++this.choice;
  }

  addPropertyName(): void {
    if (this.propertyName.match(this.reg) || this.propertyName.length < 3) {
      return;
    }

    let suggestion = {
      id: this.listNewPropertyService.getSavedPropertyId(),
      name: this.propertyName,
      login: AuthHelper.getLogin(),
    };

    fetch(`https://apartmain.azurewebsites.net/api/listnewproperty/addname`, {
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
          this.incrementChoice();
          this.getCountries();
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getCountries(): void {
    fetch('https://apartmain.azurewebsites.net/api/countries/getcountries', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          let countriesAdd = document.getElementById('countriesSelect');
          let newOption;
          let counter = 1;
          for (let item of data.countries) {
            newOption = new Option(item.title, counter.toString());
            countriesAdd?.append(newOption);
            counter++;
          }
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addPropertyAddress(): void {
    if (this.sCountry == '-1' || !this.sCountry.match(/\d/)) {
      alert("Select your country!");
      return;
    }

    let suggestion = {
      id: this.listNewPropertyService.getSavedPropertyId(),
      address: {
        countryId: this.sCountry,
        city: {
          title: this.sCity,
        },
        region: {
          title: this.sRegion,
        },
        zipCode: this.sZipCode,
        addressText: this.sAddress,
      },
      login: AuthHelper.getLogin(),
    };

    fetch(`https://apartmain.azurewebsites.net/api/listnewproperty/addaddress`, {
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
        if (data.code === 200) {
          this.router.navigate(['/lp/apartments']);
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
        this.choice = params['choice'];
        if (this.choice == 0) {
          let firstLine = document.getElementById('firstLine');
          if (firstLine !== null) {
            firstLine.classList.add('navstep__container--after');
          }

          let secondLine = document.getElementById('secondLine');
          if (secondLine !== null) {
            secondLine.classList.add('navstep__container--active');
          }
        }
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
