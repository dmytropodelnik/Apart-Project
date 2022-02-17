import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthorizationService } from '../services/authorization.service';

import AuthHelper from '../utils/authHelper';

import {
  FormBuilder,
  FormGroup,
  Validators,
  AbstractControl,
} from '@angular/forms';

@Component({
  selector: 'app-admin-auth',
  templateUrl: './admin-auth.component.html',
  styleUrls: ['./admin-auth.component.css'],
})
export class AdminAuthComponent implements OnInit {
  login: string = '';
  password: string = '';
  loginForm: FormGroup;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private authService: AuthorizationService
  ) {
    this.loginForm = this.formBuilder.group({
      login: ['', [Validators.required, Validators.minLength(8)]],
      password: ['', [Validators.required, Validators.minLength(8)]],
    });
  }
  get f() {
    return this.loginForm.controls;
  }

  loginAdmin(): void {
    let user = {
      email: this.login,
      passwordHash: this.password,
    };

    fetch('https://localhost:44381/api/admin/login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(user),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
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
                this.authService.toggleLogCondition();
                this.authService.setIsAdmin(true);

                AuthHelper.saveAuth(user.email, response);

                alert('You have successfully authenticated as an admin!');

                this.router.navigate(['/admin']);
              } else {
                alert('Token fetching error!');
              }
            })
            .catch((ex) => {
              alert(ex);
            });
        } else {
          alert('Incorrect data');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  ngOnInit(): void {}
}
