import { Component, OnInit } from '@angular/core';
import { Address } from 'src/app/models/Location/address.item';
import { Country } from 'src/app/models/Location/country.item';
import { Router } from '@angular/router';

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
    private router: Router
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
        Authorization: 'Bearer ' + AuthHelper.getToken(),
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
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(suggestion),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          alert('ok');
          this.router.navigate(['/lp/propertysetup']);
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addPropertyPhotos(): void {
    let counter = 1;
    let fData = new FormData();
    if (this.uploadedFiles != null) {
      for (let file of this.uploadedFiles) {
        fData.append('uploadedFile' + counter, file);
        counter++;
      }
    }

    fetch(
      'https://apartmain.azurewebsites.net/api/listnewproperty/addphotos?suggestionId=' + 1,
      {
        method: 'POST',
        headers: {
          Accept: 'application/json',
          Authorization: 'Bearer ' + AuthHelper.getToken(),
        },
        body: fData,
      }
    )
      .then((r) => r.json())
      .then((r) => {
        if (r.code === 200) {
          alert('Files have been successfully uploaded!');
        } else {
          alert('Uploading error!');
        }
      })
      .catch((err) => {
        alert(err);
      });
  }

  ngOnInit(): void {}
}
