import { Router } from '@angular/router';
import { RepositoryEnum } from '../enums/repositoryenum.item';

import AuthHelper from '../utils/authHelper';

export class AuthorizationService {
  private isLogged: boolean = false;
  private isAdmin: boolean = false;
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

  setLogCondition(value: boolean): void {
    this.isLogged = value;
  }

  getLogCondition(): boolean {
    return this.isLogged;
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

  async refreshAuth(isGoogleAuth: boolean = false): Promise<void> {
    let model;

    if (AuthHelper.isFacebookLogin()) {
      model = {
        username: AuthHelper.getLogin(),
        accessToken: AuthHelper.getToken(),
        isFacebookAuth: true,
      };
    } else {
      model = {
        username: AuthHelper.getLogin(),
        accessToken: AuthHelper.getToken(),
        isGoogleAuth: AuthHelper.isGoogleLogin(),
      };
    }

    if (AuthHelper.getToken()) {
      await fetch('https://localhost:44381/api/codes/refreshauth', {
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
              AuthHelper.saveFacebookAuth();
            }
            this.userImage = response.user.profile.image?.path;
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
