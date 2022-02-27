import { Component, OnInit} from '@angular/core';


import AuthHelper from '../../utils/authHelper';

@Component({
  selector: 'app-lp-name-and-location',
  templateUrl: './lp-name-and-location.component.html',
  styleUrls: ['./lp-name-and-location.component.css'],
})
export class LpNameAndLocationComponent implements OnInit {
  propertyName: string = '';
  savedPropertyId: string = '';

  constructor() {}
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
    let suggestion = {
      name: "test",  // this.propertyName,
      login: "test",  // AuthHelper.getLogin(),
    };

    fetch(`https://localhost:44381/api/listnewproperty/addname`, {
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
          this.savedPropertyId = data.savedSuggestionId;
        }
        console.log(data);
        console.log(this.savedPropertyId);
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addPropertyAddress(): void {
    let suggestion = {
      id: 1,  // this.savedPropertyId,
      address: {
        country: {
          title: "testcountry",
        },
        city: {
          title: "testcity"
        },
        zipCode: "testzipcode",
        addressText: "texttest",
      },  // this.propertyName,
      login: "test",  // AuthHelper.getLogin(),
    };

    fetch(`https://localhost:44381/api/listnewproperty/addaddress`, {
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

  ngOnInit(): void {
    this.addPropertyAddress();
  }
}
