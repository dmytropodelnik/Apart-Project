import { Component, OnInit } from '@angular/core';
import { Address } from 'src/app/models/Location/address.item';

import { ListNewPropertyService } from '../../services/list-new-property.service';

import AuthHelper from '../../utils/authHelper';

@Component({
  selector: 'app-lp-name-and-location',
  templateUrl: './lp-name-and-location.component.html',
  styleUrls: ['./lp-name-and-location.component.css'],
})
export class LpNameAndLocationComponent implements OnInit {
  savedPropertyId: string = '';
  propertyName: string = '';
  propertyAddress: Address | null = null;
  uploadedFiles: File[] | null = null;

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

  addPropertyName(): void {
    let suggestion = {
      name: 'test', // this.propertyName,
      login: 'test', // AuthHelper.getLogin(),
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
      id: 1, // this.savedPropertyId,
      address: {
        country: {
          title: 'testcountry',
        },
        city: {
          title: 'testcity',
        },
        zipCode: 'testzipcode',
        addressText: 'texttest',
      }, // this.propertyName,
      login: 'test', // AuthHelper.getLogin(),
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

  addPropertyBeds(): void {
    let suggestion = {
      id: 1, // this.savedPropertyId,
      name: 'test', // this.propertyName,
      login: 'test', // AuthHelper.getLogin(),
    };

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
        }
        console.log(data);
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addPropertyIsParkingAvailable(): void {
    let suggestion = {
      id: 1, // this.savedPropertyId,
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
      id: 1, // this.savedPropertyId,
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
      id: 1, // this.savedPropertyId,
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

  addPropertyPhotos(): void {
    let counter = 1;
    let fData = new FormData();
    if (this.uploadedFiles != null) {
      for (let file of this.uploadedFiles) {
        fData.append('uploadedFile' + counter, file);
        counter++;
      }
    }

    fetch('https://localhost:44381/api/listnewproperty/addphotos?suggestionId=' + 1, {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: fData,
    })
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

  ngOnInit(): void {
    // this.addPropertyPhotos();
  }
}
