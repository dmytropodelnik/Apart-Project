import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';

import AuthHelper from '../utils/authHelper';
import { AuthorizationService } from '../services/authorization.service';
import { Router } from '@angular/router';
import {
  FormBuilder,
  FormGroup,
  Validators,
  AbstractControl,
} from '@angular/forms';
import { RepositoryEnum } from '../enums/repositoryenum.item';

import { SocialAuthService } from 'angularx-social-login';
import { SocialUser } from 'angularx-social-login';
import { GoogleLoginProvider } from 'angularx-social-login';
import { MainDataService } from '../services/main-data.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BookingFinalStepComponent } from '../booking-properties/booking-final-step/booking-final-step.component';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css'],
})
export class AuthComponent implements OnInit {
  email: string = '';
  password: string = '';
  confirmPassword: string = '';
  verificationCode: string = '';
  isExistUser: boolean = false;
  isAccountExists: boolean = false;
  isPasswordEqual: boolean = false;
  passwordForm: FormGroup;
  codeForm: FormGroup;
  emailForm: FormGroup;
  submitted: boolean = false;
  isPasswordsEqual: boolean = false;
  letterAction: boolean = false;
  signInWithPassword: boolean = false;

  userFirstName: string = '';
  userLastName: string = '';
  image: string = '';
  facebookId: string = '';

  letterMessage: string = '';

  public user: SocialUser = new SocialUser();

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    public authService: AuthorizationService,
    private router: Router,
    private formBuilder: FormBuilder,
    private authSocialService: SocialAuthService,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {
    this.passwordForm = this.formBuilder.group(
      {
        password: ['', [Validators.required, Validators.minLength(8)]],
        confirmPassword: ['', [Validators.required]],
      },
      {
        validator: this.MustMatch('password', 'confirmPassword'),
      }
    );
    this.codeForm = this.formBuilder.group({
      verificationCode: ['', [Validators.required, Validators.minLength(6)]],
    });
    this.emailForm = this.formBuilder.group({
      email: [
        '',
        [
          Validators.required,
          Validators.email,
          Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$'),
        ],
      ],
    });
  }

  get f() {
    return this.emailForm.controls;
  }

  get f1() {
    return this.passwordForm.controls;
  }

  get f2() {
    return this.codeForm.controls;
  }

  checkSignInOption() {
    this.signInWithPassword = !this.signInWithPassword;
  }

  sendInfoLetter(): void {
    fetch(
      `https://localhost:44381/api/notifications/sendnotification?email=${this.email}&message=${this.letterMessage}&action=${this.letterAction}`,
      {
        method: 'GET',
      }
    )
      .then((r) => r.json())
      .then(async (data) => {
        if (data.code === 200) {
          document.location.href = 'https://localhost:4200';
        } else {
          this.showAlert(data.message);
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  userCheck(): void {
    let user = {
      email: this.email,
      password: this.password,
    };
    fetch(`https://localhost:44381/api/users/userexists?email=${user.email}&sendLetter=${!this.signInWithPassword}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.isAccountExists = true;
        }
        this.isExistUser = true;
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  userSignIn(): void {
    let user = {
      email: this.email,
      password: this.password,
      repository: RepositoryEnum.Enter,
    };

    fetch('https://localhost:44381/token', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
      },
      body: JSON.stringify(user),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code !== 400) {
          this.authService.setTokenKey(response.encodedJwt);
          AuthHelper.saveAuth(user.email, response.encodedJwt);
          this.authService.setLogCondition(true);
          this.showAlert('You have successfully authenticated!');

          this.router.navigate(['']);
        } else {
          this.showAlert('Token fetching error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  userSocialSignIn(): void {
    let user = {
      email: this.email,
      repository: RepositoryEnum.Enter,
    };

    fetch('https://localhost:44381/tokenforsocial', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
      },
      body: JSON.stringify(user),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code !== 400) {
          this.authService.setTokenKey(response.encodedJwt);
          AuthHelper.saveAuth(this.email, response.encodedJwt);
          this.authService.setLogCondition(true);
          this.showAlert('You have successfully authenticated!');
          this.letterMessage = `You have successfully entered on Apartstep.fun via social network with ${this.email}!`;
          this.letterAction = true;
          this.sendInfoLetter();
        } else {
          this.showAlert('Token fetching error!');
          if (AuthHelper.isFacebookLogin()) {
            AuthHelper.clearFacebookAuth();
          } else if (AuthHelper.isGoogleLogin()) {
            AuthHelper.clearGoogleAuth();
          }
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  userSignUp(): void {
    if (this.password === this.confirmPassword) {
      this.isPasswordEqual = true;
    }

    fetch(
      'https://localhost:44381/api/codes/generateregistercode?email=' +
        this.email,
      {
        method: 'GET',
      }
    )
      .then((r) => r.json())
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  loginWithPassword(): void {
    if (this.password.length == 0) {
      this.showAlert('Password length must be greater than zero!');
      return;
    }

    fetch(
      `https://localhost:44381/api/auth/loginwithpassword?email=${this.email}&password=${this.password}`,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
        },
      }
    )
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.userSignIn();
        } else {
          this.showAlert(response.message);
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  MustMatch(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];

      if (matchingControl.errors && !matchingControl.errors.mustMatch) {
        // return if another validator has already found an error on the matchingControl
        return;
      }

      // set error on matchingControl if validation fails
      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ mustMatch: true });
      } else {
        matchingControl.setErrors(null);
      }
    };
  }

  confirmEmail() {
    let user = {
      email: this.email,
      password: this.password,
      verificationCode: this.verificationCode,
    };

    fetch('https://localhost:44381/api/users/register', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
      },
      body: JSON.stringify(user),
    })
      .then((r) => r.json())
      .then((response) => {
        if (response.code === 200) {
          this.userSignIn();
          this.router.navigate(['']);
        } else {
          this.showAlert(response.code);
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  async verifyGoogleEnter(): Promise<void> {
    await this.authSocialService.signIn(GoogleLoginProvider.PROVIDER_ID);
    this.authSocialService.authState.subscribe((user) => {
      this.user = user;
      this.email = user.email;
      this.googleEnter();
    });
  }

  verifyFacebookEnter(): void {
    FB.login(
      (response) => {
        if (response.status === 'connected') {
          // Logged into your webpage and Facebook.
          this.facebookId = response.authResponse.userID;

          /* make the API call */
          FB.api('/me?fields=id,email,name,picture', (response) => {
            if (response) {
              let userName = (response as any).name as string;
              this.userFirstName = userName.substring(0, userName.indexOf(' '));
              this.userLastName = userName.substring(
                userName.indexOf(' ') + 1,
                userName.length
              );
              this.email = (response as any).email;
              this.image = (response as any).picture.data.url;
              this.authService.setUserImage(this.image);

              fetch(
                'https://localhost:44381/api/users/userexistsbysocial?email=' +
                  this.email,
                {
                  method: 'GET',
                }
              )
                .then((r) => r.json())
                .then((data) => {
                  if (data.code === 200 && data.userExisted) {
                    AuthHelper.saveFacebookAuth();
                    this.userSocialSignIn();
                  } else {
                    let person = {
                      email: this.email,
                      firstName: this.userFirstName,
                      lastName: this.userLastName,
                      image: this.image,
                    };

                    fetch(
                      'https://localhost:44381/api/users/registerviasocial',
                      {
                        method: 'POST',
                        headers: {
                          'Content-Type': 'application/json; charset=utf-8',
                        },
                        body: JSON.stringify(person),
                      }
                    )
                      .then((r) => r.json())
                      .then((response) => {
                        if (response.code === 200) {
                          this.userSocialSignIn();
                        } else {
                          this.showAlert(response.code);
                        }
                      })
                      .catch((ex) => {
                        this.mainDataService.alertContent = ex;
                        this.modalService.open(this.alert);
                      });
                  }
                })
                .catch((ex) => {
                  this.mainDataService.alertContent = ex;
                  this.modalService.open(this.alert);
                });
            } else {
              this.showAlert('Login via facebook error!');
            }
          });
        } else {
          // The person is not logged into your webpage or we are unable to tell.
          this.showAlert('Login via facebook error!');
        }
      },
      { scope: 'email, public_profile,', return_scopes: true }
    );
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
        } else {
          this.showAlert('Logout error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  googleEnter(): void {
    fetch(
      'https://localhost:44381/api/users/userexistsbysocial?email=' +
        this.user.email,
      {
        method: 'GET',
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200 && data.userExisted) {
          this.userSocialSignIn();
        } else {
          let person = {
            email: this.user.email,
            firstName: this.user.firstName,
            lastName: this.user.lastName,
            image: this.user.photoUrl,
          };

          fetch('https://localhost:44381/api/users/registerviasocial', {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json; charset=utf-8',
            },
            body: JSON.stringify(person),
          })
            .then((r) => r.json())
            .then((response) => {
              if (response.code === 200) {
                AuthHelper.saveGoogleAuth();
                this.userSocialSignIn();
              } else {
                this.showAlert(response.code);
              }
            })
            .catch((ex) => {
              this.mainDataService.alertContent = ex;
              this.modalService.open(this.alert);
            });
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  ngOnInit(): void {
    //FB.XFBML.parse();
  }
}
