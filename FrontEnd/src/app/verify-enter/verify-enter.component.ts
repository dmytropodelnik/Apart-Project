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
  code: string = '';
  isToResetPassword: boolean = false;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private authService: AuthorizationService
  ) {}

  verifyEnterUser(): void {
    fetch(
      `https://localhost:44381/api/codes/verifyenteruser?email=${this.email}&code=${this.code}&confidant=true`,
      {
        method: 'GET',
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          let user = {
            email: this.email,
            code: this.code,
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
              if (response.code !== 400) {
                this.authService.setTokenKey(response);
                AuthHelper.saveAuth(user.email, response);
                this.authService.toggleLogCondition();

                alert('You have successfully authenticated!');
                this.router.navigate(['']);
              } else {
                alert("Token fetching error!");
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
          this.router.navigate(['/resetpassword',], {
            queryParams: {
              code: this.code,
              email: this.email,
            }
          });
        } else {
          alert('Enter error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  ngOnInit(): void {
    // Note: Below 'queryParams' can be replaced with 'params' depending on your requirements
    this.activatedRoute.queryParams.subscribe((params: any) => {
      this.email = params['email'];
      this.code = params['code'];
      this.isToResetPassword = params['resetPassword'];
    });

    if (this.isToResetPassword) {
      this.resetPassword();
    } else {
      this.verifyEnterUser();
    }
  }
}
