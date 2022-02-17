import { Component, OnInit } from '@angular/core';

import AuthHelper from '../utils/authHelper';

import { AuthorizationService } from '../services/authorization.service';

@Component({
  selector: 'app-join-as-partner',
  templateUrl: './join-as-partner.component.html',
  styleUrls: ['./join-as-partner.component.css']
})
export class JoinAsPartnerComponent implements OnInit {
  authHelper: any = AuthHelper;
  login = false;

  constructor(
    public authService: AuthorizationService,
  ) {

  }

  userSignOut(): void {
    let model = {
      username: AuthHelper.getLogin(),
      accessToken: AuthHelper.getToken(),
    };

    fetch('https://localhost:44381/api/users/signoutuser', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(model),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.authService.setLogCondition(false);
          AuthHelper.clearAuth();
        } else {
          alert("Logout error!");
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }


  ngOnInit(): void {
  }

}
