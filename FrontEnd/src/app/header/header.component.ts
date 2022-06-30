import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { User } from '../models/UserData/user.item';
import { Router } from '@angular/router';

import AuthHelper from '../utils/authHelper';
import ImageHelper from '../utils/imageHelper';

import { AuthorizationService } from '../services/authorization.service';
import { SocialAuthService } from 'angularx-social-login';
import { MainDataService } from '../services/main-data.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  public isCollapsed = false;
  user: User | null = null;
  authHelper: any = AuthHelper;
  displayMonths = 2;
  navigation = 'arrows';
  showWeekNumbers = false;
  outsideDays = 'hidden';
  imageHelper: any = ImageHelper;
  isActive1 = true;

  @ViewChild('content') content!: TemplateRef<any>;
  @ViewChild('content1') content1!: TemplateRef<any>;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    config: NgbModalConfig,
    private router: Router,
    public authService: AuthorizationService,
    private authSocialService: SocialAuthService,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  getToken(): string {
    return AuthHelper.getToken();
  }

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
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
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(model),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.authService.setLogCondition(false);
          AuthHelper.clearAuth();

          FB.getLoginStatus(function (response) {
            // Called after the JS SDK has been initialized.
            if (response.status === 'connected') {
              // Returns the login status.
              FB.logout(function (response) {
                // Person is now logged out
              });
            }
          });

          if (AuthHelper.isFacebookLogin()) {
            AuthHelper.clearFacebookAuth();
          }
          if (AuthHelper.isGoogleLogin()) {
            this.authSocialService.signOut();
            AuthHelper.clearGoogleAuth();
          }

          this.router.navigate(['']);
        } else {
          this.showAlert('Logout error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  ngOnInit(): void {}

  open() {
    this.modalService.open(this.content, { size: 'lg', centered: true });
  }

  open1() {
    this.modalService.open(this.content1, { size: 'lg', centered: true });
  }
}
