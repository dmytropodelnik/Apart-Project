import { Component, OnInit } from '@angular/core';

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
import { ConsoleLogger } from '@angular/compiler-cli/private/localize';

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
  isExistUser = false;
  isAccountExists = false;
  isPasswordEqual = false;
  passwordForm: FormGroup;
  codeForm: FormGroup;
  emailForm: FormGroup;
  submitted = false;
  isPasswordsEqual = false;

  userFirstName: string = "";
  userLastName: string = "";
  facebookId: string = "";

  constructor(
    private authService: AuthorizationService,
    private router: Router,
    private formBuilder: FormBuilder
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
    FB.XFBML.parse();
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

  userCheck(): void {
    let user = {
      email: this.email,
      password: this.password,
    };
    console.log(user);
    fetch(`https://localhost:44381/api/users/userexists?email=${user.email}`, {
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
        console.log(data);
      })
      .catch((ex) => {
        alert(ex);
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
          this.authService.toggleLogCondition();
          alert('You have successfully authenticated!');
          this.router.navigate(['']);
        } else {
          alert('Token fetching error!');
        }
      })
      .catch((ex) => {
        alert(ex);
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
      .then((data) => {
        alert(data.code);
        console.log(data);
      })
      .catch((ex) => {
        alert(ex);
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
        alert(response.code);
        console.log(response);

        if (response.code === 200) {
          this.userSignIn();
          this.router.navigate(['']);
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  verifyFacebookEnter() {
    FB.login(
      (response) => {
        if (response.status === 'connected') {
          // Logged into your webpage and Facebook.
          this.facebookId = response.authResponse.userID;

          /* make the API call */
          FB.api(response.authResponse.userID, (response) => {
            if (response) {
              let userName = (response as any).name as string;
              this.userFirstName = userName.substring(0, userName.indexOf(' '));
              this.userLastName = userName.substring(userName.indexOf(' ') + 1, userName.length);
            }
          });

          fetch('https://localhost:44381/api/users/userexistsbyfacebook?id=' + this.facebookId, {
              method: 'GET',
            }
          )
            .then((r) => r.json())
            .then((data) => {
              if (data.code === 200 && data.userExisted) {
                let user = {
                  facebookId: this.facebookId,
                  repository: RepositoryEnum.Enter,
                };

                fetch('https://localhost:44381/tokenforfacebook', {
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
                      AuthHelper.saveAuth(user.facebookId, response.encodedJwt);
                      this.authService.toggleLogCondition();
                      this.authService.setFacebookAuthCondition(true);
                      alert('You have successfully authenticated!');
                      console.log('You have successfully authenticated!');
                      this.router.navigate(['']);
                    } else {
                      alert('Token fetching error!');
                    }
                  })
                  .catch((ex) => {
                    alert(ex);
                  });
              } else {
                let person = {
                  facebookId: this.facebookId,
                  firstName: this.userFirstName,
                  lastName: this.userLastName,
                };

                fetch('https://localhost:44381/api/users/registerviafacebook', {
                  method: 'POST',
                  headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                  },
                  body: JSON.stringify(person),
                })
                  .then((r) => r.json())
                  .then((response) => {
                    if (response.code === 200) {
                      this.userSignIn();
                    } else {
                      alert(response.code);
                    }
                  })
                  .catch((ex) => {
                    alert(ex);
                  });
              }
            })
            .catch((ex) => {
              alert(ex);
            });
        } else {
          // The person is not logged into your webpage or we are unable to tell.
          alert('Login via facebook error!');
        }
      },
      { scope: 'public_profile,email' }
    );
  }

  ngOnInit(): void {
    //FB.XFBML.parse();
  }
}
