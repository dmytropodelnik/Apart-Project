import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Facility } from 'src/app/models/facility.item';
import { SuggestionRule } from 'src/app/models/Suggestions/suggestionrule.item';

import { ListNewPropertyService } from '../../services/list-new-property.service';

import AuthHelper from '../../utils/authHelper';

@Component({
  selector: 'app-lp-property-setup',
  templateUrl: './lp-property-setup.component.html',
  styleUrls: ['./lp-property-setup.component.css'],
})
export class LpPropertySetupComponent implements OnInit {
  savedPropertyId: string = '';
  facilities: Facility[] | null = null;
  includedFacilities: boolean[] = [];

  parking: boolean = false;
  starsRating: number = 0;

  languages = [false, false, false, false, false, false, false, false];
  correctLanguages = [
    'English',
    'Ukrainian',
    'German',
    'French',
    'Russian',
    'Spanish',
    'Italian',
    'Arabic',];

  rules: SuggestionRule[] | null = null;
  includedRules: boolean[] = [];

  description: string = '';

  constructor(
    private listNewPropertyService: ListNewPropertyService,
    private router: Router,
  ) {

  }
  choice: number = 1;
  bedTypesAmount = [0, 0, 0, 0, 0, 0, 0, 0, 0];

  decreaseBedTypeCount(value: number) {
    --this.bedTypesAmount[value];
  }

  increaseBedTypeCount(value: number) {
    ++this.bedTypesAmount[value];
  }

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

  setParking(value: boolean): void {
    this.parking = value;
  }

  setCorrectLanguages(): void {
    for (let i = 0, j = 0; i < this.languages.length; i++, j++) {
      if (this.languages[i] === false) {
        this.correctLanguages.splice(j, 1);
        j--;
      }
    }
  }

  addPropertyBeds(): void {
    let suggestion = {
      id: this.listNewPropertyService.getSavedPropertyId(),
      beds: [{}],
      login: AuthHelper.getLogin(),
    };

    fetch(`https://localhost:44381/api/bedtypes/getbedtypes`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          suggestion.beds.pop();
          for (let i = 0; i < data.bedTypes.length; i++) {
            suggestion.beds.push({
              bedTypeId: i + 1,
              amount: this.bedTypesAmount[i],
            });
          }
        }
      })
      .then(r => {
        fetch(`https://localhost:44381/api/listnewproperty/addbeds`, {
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
              this.incrementChoice();
            }
          })
          .catch((ex) => {
            alert(ex);
          });
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addPropertyIsParkingAvailable(): void {
    let suggestion = {
      id: this.listNewPropertyService.getSavedPropertyId(),
      isParkingAvailable: this.parking,
    };

    fetch(`https://localhost:44381/api/listnewproperty/addparking`, {
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
          this.incrementChoice1();
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addPropertyLanguages(): void {
    this.setCorrectLanguages();

    let suggestion = {
      id: this.listNewPropertyService.getSavedPropertyId(),
      languages: this.correctLanguages,
    };

    fetch(`https://localhost:44381/api/listnewproperty/addlanguages`, {
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
          this.incrementChoice1();
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addPropertyRules(): void {
    let suggestion = {
      id: this.listNewPropertyService.getSavedPropertyId(),
      suggestionRules: this.includedRules,
    };

    fetch(`https://localhost:44381/api/listnewproperty/addrules`, {
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
          // this.router.navigate(['/lp/photos']);
          this.incrementChoice1();
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addPropertyFacilities(): void {
    let suggestion = {
      id: this.listNewPropertyService.getSavedPropertyId(),
      facilities: this.includedFacilities,
    };

    fetch(`https://localhost:44381/api/listnewproperty/addfacilities`, {
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
          this.incrementChoice1();
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getFacilities(): void {
    fetch(`https://localhost:44381/api/facilities/getfacilities`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.facilities = data.facilities;
          if (this.facilities !== null) {
            for (let item of this.facilities) {
              this.includedFacilities.push(false);
            }
          } else {
            alert('Facilities fetching error!');
          }
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getRules(): void {
    fetch(`https://localhost:44381/api/suggestionrules/getrules`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.rules = data.rules;
          if (this.rules !== null) {
            for (let item of this.rules) {
              this.includedRules.push(false);
            }
          } else {
            alert('Rules fetching error!');
          }
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addDescription(): void {
    let suggestion = {
      id: this.listNewPropertyService.getSavedPropertyId(),
      description: this.description,
    };

    console.log(this.description);

    fetch(`https://localhost:44381/api/listnewproperty/adddescription`, {
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
          this.incrementChoice1();
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addStarsRating(): void {
    let suggestion = {
      id: this.listNewPropertyService.getSavedPropertyId(),
      starsRating: this.starsRating,
    };

    fetch(`https://localhost:44381/api/listnewproperty/addstarsrating`, {
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
          this.router.navigate(['/lp/photos']);
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  ngOnInit(): void {
    this.getFacilities();
    this.getRules();
  }
}
