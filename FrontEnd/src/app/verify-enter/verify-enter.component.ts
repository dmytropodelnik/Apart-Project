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

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private authService: AuthorizationService
  ) {}

  ngOnInit(): void {
    // Note: Below 'queryParams' can be replaced with 'params' depending on your requirements
    this.activatedRoute.queryParams.subscribe((params: any) => {
      this.email = params['email'];
      this.code = params['code'];
    });
    console.log(this.email);
    console.log(this.code);

    fetch(
      `https://localhost:44381/api/codes/verifyenteruser?email=${this.email}&code=${this.code}`,
      {
        method: 'GET',
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          let user = {
            email: this.email,
            password: "123",
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
        } else {
          alert('Enter error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }
}
