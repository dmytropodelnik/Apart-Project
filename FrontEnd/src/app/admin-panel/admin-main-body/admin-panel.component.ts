import { Component, OnInit, OnDestroy, HostListener } from '@angular/core';
import { Router } from '@angular/router';

import { AuthorizationService } from '../../services/authorization.service';

import AuthHelper from '../../utils/authHelper';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit, OnDestroy {
  currentYear: number = new Date().getFullYear();
  content: string | undefined;

  constructor(
    private router: Router,
    private authService: AuthorizationService,
  ) {

  }

  setContent(newContent: string) {
    this.content = newContent;
  }

  async userSignOut(): Promise<void> {
    let model = {
      userName: AuthHelper.getLogin(),
      accessToken: AuthHelper.getToken(),
    };

    fetch('https://apartmain.azurewebsites.net/api/users/signoutuser', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(model),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.authService.setLogCondition(false);
          this.authService.setIsAdmin(false);
          AuthHelper.clearAuth();

          alert("Success logout!");
          document.location.href="https://localhost:4200";
        } else {
          alert("Logout error!");
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  ngOnInit(): void {
    // if (!this.authService.getIsAdmin()      ||
    //     !this.authService.getLogCondition()) {
    //       this.router.navigate(['']);
    // }
  }

  @HostListener('window:beforeunload')
  async ngOnDestroy() {
    this.userSignOut();
  }

}
