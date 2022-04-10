import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthorizationService } from '../services/authorization.service';

import AuthHelper from '../utils/authHelper';

@Component({
  selector: 'app-verify-enter',
  templateUrl: './verify-enter.component.html',
  styleUrls: ['./verify-enter.component.css'],
})
export class VerifyEnterComponent implements OnInit {
  email: string = '';
  oldEmail: string = '';
  code: string = '';
  isToResetPassword: boolean = false;
  isToChangeEmail: boolean = false;
  isToDeleteCode: boolean = false;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private authService: AuthorizationService
  ) {}

  async verifyEnterUser(): Promise<void> {
    fetch(
      `https://localhost:44381/api/codes/verifyenteruser?email=${this.email}&code=${this.code}&confidant=true`,
      {
        method: 'GET',
      }
    )
      .then((r) => r.json())
      .then(async (data) => {
        if (data.code === 200) {
          if (this.isToChangeEmail) {
            await this.saveEmail();
          }

          let user = {
            email: this.email,
            code: this.code,
            isToDeleteCode: this.isToDeleteCode,
          };

          fetch('https://localhost:44381/token', {
            method: 'POST',
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
                this.authService.setTokenKey(response.encodedJwt);
                AuthHelper.saveAuth(response.email, response.encodedJwt);
                this.authService.toggleLogCondition();

                alert('You have successfully authenticated!');

                if (this.isToChangeEmail) {
                  this.router.navigate(['/mysettings']);
                } else if (!this.isToResetPassword) {
                  this.router.navigate(['']);
                }
              } else {
                alert('Token fetching error!');
              }
            })
            .catch((ex) => {
              alert(ex);
            });
        } else {
          alert('Enter error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  resetPassword(): void {
    fetch(
      `https://localhost:44381/api/codes/verifyenteruser?email=${this.email}&code=${this.code}&confidant=true`,
      {
        method: 'GET',
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          alert('Redirecting to reseting page!');
          this.router.navigate(['/resetpassword']);
        } else {
          alert('Enter error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  async saveEmail(): Promise<void> {
    let user = {
      email: this.oldEmail,
      newEmail: this.email,
    };

    await fetch('https://localhost:44381/api/userdataeditor/editemail', {
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
          this.email = response.resUser.email;
          alert('You have successfully changed your email!');
        } else {
          alert('Save email error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  async ngOnInit(): Promise<void> {
    // Note: Below 'queryParams' can be replaced with 'params' depending on your requirements
    this.activatedRoute.queryParams.subscribe(async (params: any) => {
      this.email = params['email'];
      this.oldEmail = params['oldEmail'];
      this.code = params['code'];
      this.isToResetPassword = params['resetPassword'];
      this.isToChangeEmail = params['changeEmail'];
      this.isToDeleteCode = params['isToDeleteCode'];

      await this.verifyEnterUser();

      if (this.isToResetPassword) {
        this.resetPassword();
      }
    });
  }
}
