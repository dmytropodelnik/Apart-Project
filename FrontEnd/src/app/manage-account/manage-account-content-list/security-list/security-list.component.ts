import { Component, OnInit } from '@angular/core';

import AuthHelper from '../../../utils/authHelper';
import ImageHelper from '../../../utils/imageHelper';

import { AuthorizationService } from '../../../services/authorization.service';
import { UserData } from 'src/app/view-models/userdata.item';

@Component({
  selector: 'app-security-list',
  templateUrl: './security-list.component.html',
  styleUrls: ['./security-list.component.css']
})
export class SecurityListComponent implements OnInit {
  isEditing: boolean[] = [];
  isDisabled: boolean[] = [];

  authHelper: any = AuthHelper;

  isEmailSent: boolean = false;

  user: UserData = new UserData();

  constructor(public authService: AuthorizationService) {

  }

  setCondition(id: number): void {
    this.isEditing[id] = !this.isEditing[id];
  }

  editButtonClick(id: number): void {
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

  sendResetPasswordEmail(id: number): void {
    fetch(`https://localhost:44381/api/codes/generateresetcode?email=` + AuthHelper.getLogin(), {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.isEmailSent = true;
        } else {
          alert("Error generating reset link");
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteAccount(id: number): void {
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

  initializeBoolArray(): void {
    for (let i = 0; i < 2; i++) {
      this.isDisabled[i] = false;
    }
  }

  ngOnInit(): void {
    this.initializeBoolArray();
  }

}
