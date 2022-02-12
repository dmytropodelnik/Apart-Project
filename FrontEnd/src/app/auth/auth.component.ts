import { Component, OnInit } from '@angular/core';

import AuthHelper from '../utils/authHelper';
import { AuthorizationService } from '../services/authorization.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, AbstractControl} from '@angular/forms';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css'],
})
export class AuthComponent implements OnInit {
  email: string  = '';
  password: string = '';
  confirmPassword: string = '';
  verificationCode : string = '';
  isExistUser = false;
  isAccountExists = false;
  isPasswordEqual = false;
  passwordForm: FormGroup;
  codeForm: FormGroup;
  emailForm: FormGroup;
  submitted = false;
  isPasswordsEqual = false;

  constructor(
    private authService: AuthorizationService,
    private router: Router,
    private formBuilder: FormBuilder
  ) {
    this.passwordForm = this.formBuilder.group({
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', [Validators.required]],
    },{
      validator: this.MustMatch('password','confirmPassword')
    }
    );
    this.codeForm = this.formBuilder.group({
      verificationCode: ['', [Validators.required, Validators.minLength(6)]]
    });
    this.emailForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email, Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')]]
    });
  }
  get f() {return this.emailForm.controls; }
  get f1() { return this.passwordForm.controls; }
  get f2() { return this.codeForm.controls; }

  userCheck(): void {
    let user = {
      email: this.email,
      password: this.password,
    };
    fetch(`https://apartproject.azurewebsites.net/api/users/userexists?email=${user.email}`, {
      method: 'GET',
      headers: {
        "Accept": "application/json",
        "Authorization": "Bearer " + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code == 200) {
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
    };

    fetch('https://apartproject.azurewebsites.net/token', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        "Accept": "application/json",
        "Authorization": "Bearer " + AuthHelper.getToken(),
      },
      body: JSON.stringify(user),
    })
      .then(response => response.json())
      .then(response => {
        AuthHelper.saveAuth(user.email, response);
        this.authService.toggleLogCondition();

        alert('You have successfully authenticated!');
        this.router.navigate(['']);
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  userSignUp(): void {
    if (this.password === this.confirmPassword){
      this.isPasswordEqual = true;
    }

    let user = {
      email: this.email,
    };

    fetch('https://apartproject.azurewebsites.net/api/codes/generateregistercode', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        "Accept": "application/json",
        "Authorization": "Bearer " + AuthHelper.getToken(),
       },
      body: JSON.stringify(this.email),
    })
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
    }
}
  confirmEmail() {
    let user = {
      email: this.email,
      password: this.password,
      verificationCode: this.verificationCode,
    };

    fetch('https://apartproject.azurewebsites.net/api/users/register', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        "Accept": "application/json",
        "Authorization": "Bearer " + AuthHelper.getToken(),
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

  verifyEnter() {

  }

  ngOnInit(): void {}

}
