import { Component, OnInit } from '@angular/core';

import { AuthorizationService } from '../../services/authorization.service';

import AuthHelper from '../../utils/authHelper';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {


  currentYear: number = new Date().getFullYear();
  content: string | undefined;

  constructor(
    private authService: AuthorizationService
  ) {

  }

  setContent(newContent: string) {
    this.content = newContent;
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
          this.authService.setIsAdmin(false);
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
