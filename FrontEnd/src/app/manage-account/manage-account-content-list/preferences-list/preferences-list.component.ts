import { Component, OnInit } from '@angular/core';

import AuthHelper from '../../../utils/authHelper';
import ImageHelper from '../../../utils/imageHelper';

import { AuthorizationService } from '../../../services/authorization.service';
import { UserData } from 'src/app/view-models/userdata.item';

@Component({
  selector: 'app-preferences-list',
  templateUrl: './preferences-list.component.html',
  styleUrls: ['./preferences-list.component.css'],
})
export class PreferencesListComponent implements OnInit {
  isEditing: boolean[] = [];
  isDisabled: boolean[] = [];

  user: UserData = new UserData();

  constructor(private authService: AuthorizationService) {

  }

  setCondition(id: number): void {
    this.isEditing[id] = !this.isEditing[id];
  }

  editButtonClick(id: number): void {
    this.getCurrencies();
    this.getLanguages();
    this.setCondition(id);
    this.setConditionEditButtons(id, true);
  }

  saveButtonClick(id: number): void {
    this.setCondition(id);
    this.setConditionEditButtons(id, false);
  }

  cancelButtonClick(id: number): void {
    this.setCondition(id);
    this.setConditionEditButtons(id, false);
  }

  setConditionEditButtons(id: number, value: boolean): void {
    for (let i = 0; i < this.isDisabled.length; i++) {
      if (i !== id) {
        this.isDisabled[i] = value;
      }
    }
  }

  saveCurrency(id: number): void {
    let user = {
      currency: this.user.currency.abbreviation,
      email: AuthHelper.getLogin(),
    };

    fetch('https://localhost:44381/api/userdataeditor/editcurrency', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(user),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.user.currency = response.resProfile.currency;
          this.setCondition(id);
          this.setConditionEditButtons(id, false);
        } else {
          alert('Save currency error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  saveLanguage(id: number): void {
    let user = {
      languageId: this.user.language,
      email: AuthHelper.getLogin(),
    };

    fetch('https://localhost:44381/api/userdataeditor/editlanguage', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(user),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.user.language = response.resProfile.language.title;
          this.setCondition(id);
          this.setConditionEditButtons(id, false);
        } else {
          alert('Save language error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getCurrentUser(): void {
    fetch(
      'https://localhost:44381/api/users/getuser?email=' +
        AuthHelper.getLogin(),
      {
        method: 'GET',
      }
    )
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.user.currency = response.user.profile.currency;
          this.user.language = response.user.profile.language.title;
        } else {
          alert('Get current user error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  initializeBoolArray(): void {
    for (let i = 0; i < 2; i++) {
      this.isDisabled[i] = false;
    }
  }

  getCurrencies(): void {
    fetch(`https://localhost:44381/api/currencies/getcurrencies`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
            let currenciesSelect = document.getElementById('currenciesSelect');
            let newOption;
            for (let item of data.currencies) {
              newOption = new Option(
                item.abbreviation,
                item.abbreviation
              );
              currenciesSelect?.append(newOption);
            }
        } else {
          alert('Get currencies error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getLanguages(): void {
    fetch(`https://localhost:44381/api/languages/getlanguages`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          let languagesSelect = document.getElementById('languagesSelect');
          let newOption;
          let counter = 1;
          for (let item of data.languages) {
            newOption = new Option(item.title, counter.toString());
            languagesSelect?.append(newOption);
            counter++;
          }
        } else {
          alert('Get languages error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  ngOnInit(): void {
    this.initializeBoolArray();
    this.getCurrentUser();
  }
}
