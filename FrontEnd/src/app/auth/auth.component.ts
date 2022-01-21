import { Component, OnInit } from '@angular/core';

import AuthHelper from '../utils/authHelper';
import { AuthorizationService } from '../services/authorization.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css'],
})
export class AuthComponent implements OnInit {
  email: string;
  password: string;
  isExist = false;

  constructor(
    private authService: AuthorizationService,
    private router: Router
  ) {
    this.email = '';
    this.password = '';
  }

  userCheck(): void {
    let user = {
      email: this.email,
      password: this.password,
    };

    fetch('https://localhost:44381/api/user/userexists?email=' + user.email, {
      method: 'GET',
    })
      .then((r) => r.text())
      .then((data) => {
        if (data === 'true') {
          this.isExist = true;
        }
        alert(data);
        alert(this.isExist);
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

  ngOnInit(): void {}
}
