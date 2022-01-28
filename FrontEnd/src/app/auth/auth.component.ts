import { Component, OnInit } from '@angular/core';

import AuthHelper from '../utils/authHelper';
import { AuthorizationService } from '../services/authorization.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

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
  registerForm: FormGroup;
  submitted = false;
  isPasswordsEqual = false;

  constructor(
    private authService: AuthorizationService,
    private router: Router,
    private formBuilder: FormBuilder
  ) {
    this.registerForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email,Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(6)]]
    });
  }
  get f() { return this.registerForm.controls; }

  userCheck(): void {
    if (!this.email.match('^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$')) {
      return;
    }
    let user = {
      email: this.email,
      password: this.password,
    };
    fetch(`https://localhost:44381/api/users/userexists?email=${user.email}`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code == 200) {
          this.isAccountExists = true;
        }
        this.isExistUser = true;
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

    fetch('https://localhost:44381/token', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
      },
      body: JSON.stringify(user),
    })
      .then(response => response.json())
      .then(response => {
        alert(response);
        console.log(response);

        console.log(this.authService.getLogCondition());

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
    if (this.password === this.confirmPassword) {
      this.isPasswordEqual = true;
    } else {
      return;
    }

    let user = {
      email: this.email,
    };

    fetch('https://localhost:44381/api/codes/generateregistercode', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
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

  confirmEmail() {
    let user = {
      email: this.email,
      password: this.password,
      verificationCode: this.verificationCode,
    };

    fetch('https://localhost:44381/api/users/register', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
       },
      body: JSON.stringify(user),
    })
    .then((r) => r.json())
    .then((response) => {
        alert(response.code);
        console.log(response);

        if (response.code === 200) {
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

