import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';

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
  @Output() onChanged = new EventEmitter<string>();
  changeEmail(setting: string) {
      this.onChanged.emit(setting);
  }

  isEditing: boolean[] = [];
  isDisabled: boolean[] = [];

  authHelper: any = AuthHelper;

  isDeleteRequested: boolean = false;
  isEmailSent: boolean = false;

  deleteReason: number = 0;

  user: UserData = new UserData();

  constructor(
    public authService: AuthorizationService,
    private router: Router) {

  }

  setDeleteReason(value: number) {
    this.deleteReason = value;
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
    this.deleteReason = 0;
  }

  setConditionEditButtons(id: number, value: boolean): void {
    for (let i = 0; i < this.isDisabled.length; i++) {
      if (i !== id) {
        this.isDisabled[i] = value;
      }
    }
  }

  sendResetPasswordEmail(id: number): void {
    fetch(`https://apartmain.azurewebsites.net/api/codes/generateresetcode?email=` + AuthHelper.getLogin(), {
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

  unsubscribeMails(id: number): void {

    this.cancelButtonClick(id);

    alert("You have successfully unsubscribed from mail letters!");
  }

  deleteAccount(id: number): void {
    fetch(`https://apartmain.azurewebsites.net/api/codes/generatedeleteusercode?email=` + AuthHelper.getLogin(), {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.isDeleteRequested = true;
          this.cancelButtonClick(id);
        } else {
          alert("Error generating delete user code");
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
