import { Component, OnInit } from '@angular/core';

import AuthHelper from '../utils/authHelper';

import { AuthorizationService } from '../services/authorization.service';

@Component({
  selector: 'app-join-as-partner',
  templateUrl: './join-as-partner.component.html',
  styleUrls: ['./join-as-partner.component.css']
})
export class JoinAsPartnerComponent implements OnInit {

  login = false;
  constructor(
    public authService: AuthorizationService,
  ) {

  }

  logout(): void {
    let model = {
      username: AuthHelper.getLogin(),
      accessToken: AuthHelper.getToken(),
    };

    fetch('https://apartproject.azurewebsites.net/users/logout', {
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
          alert("Refresh auth error!");
        }

      })
      .catch((ex) => {
        alert(ex);
      });
  }


  ngOnInit(): void {
  }

}
