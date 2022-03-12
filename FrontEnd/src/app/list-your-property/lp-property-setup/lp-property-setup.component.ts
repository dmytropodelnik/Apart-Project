import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Facility } from 'src/app/models/facility.item';

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

  addPropertyBeds(): void {
    let suggestion = {
      id: this.listNewPropertyService.getSavedPropertyId(),
      beds: [{}],
      login: AuthHelper.getLogin(),
    };
    console.log(this.listNewPropertyService.getSavedPropertyId());

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
              this.incrementChoice()
            }
            console.log(data);
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
      isParkingAvailable: true,
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
        }
        console.log(data);
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addPropertyLanguages(): void {
    let suggestion = {
      id: this.listNewPropertyService.getSavedPropertyId(),
      languages: null, //
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
        }
        console.log(data);
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addPropertyRules(): void {
    let suggestion = {
      id: this.listNewPropertyService.getSavedPropertyId(),
      suggestionRules: null, //
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
        }
        console.log(data);
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addPropertyFacilities(): void {
    let suggestion = {
      id: 1,
      facilities: this.includedFacilities, //
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

  ngOnInit(): void {
    this.getFacilities();
  }
}
