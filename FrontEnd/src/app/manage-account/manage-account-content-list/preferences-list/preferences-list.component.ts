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

  tempValue: string = '';

  user: UserData = new UserData();

  constructor(private authService: AuthorizationService) {}

  setCondition(id: number): void {
    this.isEditing[id] = !this.isEditing[id];
  }

  toggleUserMailings(value: boolean): void {
    if (value) {
      fetch(
        'https://localhost:44381/api/deals/addsubscriber?email=' +
          AuthHelper.getLogin(),
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json; charset=utf-8',
            Accept: 'application/json',
            Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
          },
        }
      )
        .then((response) => response.json())
        .then((response) => {
          if (response.code === 200) {
            this.user.hasMailing = true;
          } else {
            alert('Add subscriber error!');
          }
        })
        .catch((ex) => {
          alert(ex);
        });
    } else {
      fetch(
        'https://localhost:44381/api/deals/removesubscriber?email=' +
          AuthHelper.getLogin(),
        {
          method: 'DELETE',
          headers: {
            'Content-Type': 'application/json; charset=utf-8',
            Accept: 'application/json',
            Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
          },
        }
      )
        .then((response) => response.json())
        .then((response) => {
          if (response.code === 200) {
            this.user.hasMailing = false;
          } else {
            alert('Remove subscriber error!');
          }
        })
        .catch((ex) => {
          alert(ex);
        });
    }
  }

  editButtonClick(id: number, value: any): void {
    this.tempValue = value;
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
    if (id == 0) {
      this.user.currency = this.tempValue;
    } else if (id == 1) {
      this.user.language = this.tempValue;
    }

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
      currency: this.user.currency,
      email: AuthHelper.getLogin(),
    };

    fetch('https://localhost:44381/api/userdataeditor/editcurrency', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(user),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.user.currency = response.resProfile.currency.abbreviation;
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
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
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

  async getCurrentUser(): Promise<void> {
    if (!this.authService.isGotData) {
      await this.authService.refreshAuth();
    }
    fetch(
      'https://localhost:44381/api/users/getuser?email=' +
        AuthHelper.getLogin(),
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.authService.isGotData = true;
          this.authService.setUserImage(response.user.profile.image?.path);
          this.user.hasMailing = response.user.profile.hasMailing;
          this.user.currency = response.user?.profile?.currency.abbreviation;
          this.user.language = response.user?.profile?.language?.title;
        } else {
          alert('Get current user error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  initializeBoolArray(): void {
    for (let i = 0; i < 3; i++) {
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
            newOption = new Option(item.abbreviation, item.abbreviation);
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

  async ngOnInit(): Promise<void> {
    this.initializeBoolArray();
    await this.getCurrentUser();
  }
}
