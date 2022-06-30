import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';

import AuthHelper from '../../../utils/authHelper';
import ImageHelper from '../../../utils/imageHelper';

import { AuthorizationService } from '../../../services/authorization.service';
import { UserData } from 'src/app/view-models/userdata.item';
import { MainDataService } from 'src/app/services/main-data.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-preferences-list',
  templateUrl: './preferences-list.component.html',
  styleUrls: ['./preferences-list.component.css'],
})
export class PreferencesListComponent implements OnInit {
  isEditing: boolean[] = [];
  isDisabled: boolean[] = [];

  letterAction: boolean = false;

  tempValue: string = '';
  letterMessage: string = '';

  user: UserData = new UserData();

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private authService: AuthorizationService,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  setCondition(id: number): void {
    this.isEditing[id] = !this.isEditing[id];
  }

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  sendInfoLetter(): void {
    fetch(
<<<<<<< HEAD
      `https://apartmain.azurewebsites.net/api/notifications/sendnotification?email=${AuthHelper.getLogin()}&message=${this.letterMessage}&action=${this.letterAction}`,
=======
      `https://localhost:44381/api/notifications/sendnotification?email=${AuthHelper.getLogin()}&message=${
        this.letterMessage
      }&action=${this.letterAction}`,
>>>>>>> backend
      {
        method: 'GET',
      }
    )
      .then((r) => r.json())
      .then(async (data) => {
        if (data.code === 200) {
        } else {
          this.showAlert(data.message);
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  toggleUserMailings(value: boolean): void {
    if (value) {
      fetch(
        'https://apartmain.azurewebsites.net/api/deals/addsubscriber?email=' +
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
            this.showAlert('Add subscriber error!');
          }
        })
        .catch((ex) => {
          this.mainDataService.alertContent = ex;
          this.modalService.open(this.alert);
        });
    } else {
      fetch(
        'https://apartmain.azurewebsites.net/api/deals/removesubscriber?email=' +
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
            this.showAlert('Remove subscriber error!');
          }
        })
        .catch((ex) => {
          this.mainDataService.alertContent = ex;
          this.modalService.open(this.alert);
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
    if (!this.user.currency) {
      this.showAlert('Select your currency!');
      return;
    }

    let user = {
      currency: this.user.currency,
      email: AuthHelper.getLogin(),
    };

    fetch('https://apartmain.azurewebsites.net/api/userdataeditor/editcurrency', {
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

          this.letterMessage = `Your profile currency was successfully changed!`;
          this.letterAction = false;
          this.sendInfoLetter();
        } else {
          this.showAlert('Save currency error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  saveLanguage(id: number): void {
    if (this.user.language == '-1' || !this.user.language.match(/\d/)) {
      this.showAlert('Select your language!');
      return;
    }

    let user = {
      languageId: this.user.language,
      email: AuthHelper.getLogin(),
    };

    fetch('https://apartmain.azurewebsites.net/api/userdataeditor/editlanguage', {
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

          this.letterMessage = `Your profile language was successfully changed!`;
          this.letterAction = false;
          this.sendInfoLetter();
        } else {
          this.showAlert('Save language error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  async getCurrentUser(): Promise<void> {
    if (!this.authService.isGotData) {
      await this.authService.refreshAuth();
    }
    fetch(
      'https://apartmain.azurewebsites.net/api/users/getuser?email=' +
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
          this.showAlert('Get current user error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  initializeBoolArray(): void {
    for (let i = 0; i < 3; i++) {
      this.isDisabled[i] = false;
    }
  }

  getCurrencies(): void {
    fetch(`https://apartmain.azurewebsites.net/api/currencies/getcurrencies`, {
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
          this.showAlert('Get currencies error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  getLanguages(): void {
    fetch(`https://apartmain.azurewebsites.net/api/languages/getlanguages`, {
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
          this.showAlert('Get languages error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  async ngOnInit(): Promise<void> {
    this.initializeBoolArray();
    await this.getCurrentUser();
  }
}
