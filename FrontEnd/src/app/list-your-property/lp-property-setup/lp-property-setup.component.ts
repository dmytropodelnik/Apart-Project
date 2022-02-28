import { Component, OnInit } from '@angular/core';

import { ListNewPropertyService } from '../../services/list-new-property.service';

import AuthHelper from '../../utils/authHelper';

@Component({
  selector: 'app-lp-property-setup',
  templateUrl: './lp-property-setup.component.html',
  styleUrls: ['./lp-property-setup.component.css'],
})
export class LpPropertySetupComponent implements OnInit {
  savedPropertyId: string = '';

  constructor(
    private listNewPropertyService: ListNewPropertyService,
  ) {

  }
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

  addPropertyFacilities(): void {
    let suggestion = {
      id: 1, // this.savedPropertyId,
      facilities: null, //
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

  ngOnInit(): void {}
}
