import { Router } from '@angular/router';
import { RepositoryEnum } from '../enums/repositoryenum.item';

import AuthHelper from '../utils/authHelper';

export class AuthorizationService {
  private isLogged: boolean = false;
  private isAdmin: boolean = false;
  private isFacebookAuth: boolean = false;
  private token: string | null = null;
  private userImage: string = '';

  public isGotData: boolean = false;

  constructor() {
    if (!this.isGotData) {
      this.refreshAuth();
    }
  }

  getIsAdmin(): boolean {
    return this.isAdmin;
  }

  getUserImage(): string {
    return this.userImage;
  }

  setUserImage(value: string): void {
    this.userImage = value;
  }

  setIsAdmin(value: boolean): void {
    this.isAdmin = value;
  }

  setFacebookAuthCondition(value: boolean): void {
    this.isFacebookAuth = value;
  }

  setLogCondition(value: boolean): void {
    this.isLogged = value;
  }

  getLogCondition(): boolean {
    return this.isLogged;
  }

  getFacebookAuthCondition(): boolean {
    return this.isFacebookAuth;
  }

  getTokenKey(): string | null {
    return this.token;
  }

  setTokenKey(value: string): void {
    this.token = value;
  }

  toggleLogCondition(): void {
    this.isLogged = !this.isLogged;
  }

  async refreshAuth(): Promise<void> {
    let model;

    if (this.isFacebookAuth) {
      model = {
        username: AuthHelper.getLogin(),
        accessToken: AuthHelper.getToken(),
        isFacebookAuth: true,
      };
    } else {
      model = {
        username: AuthHelper.getLogin(),
        accessToken: AuthHelper.getToken(),
      };
    }

    if (AuthHelper.getToken()) {
      await fetch('https://apartmain.azurewebsites.net/api/codes/refreshauth', {
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
            this.setLogCondition(true);
            if (response.facebookAuth == true) {
              this.isFacebookAuth = true;
            }
            this.userImage = response.user.profile.image.path;
            this.isGotData = true;
          } else {
            alert("Refresh auth error!");
          }
        })
        .catch((ex) => {
          alert(ex);
        });
    }
  }
}
