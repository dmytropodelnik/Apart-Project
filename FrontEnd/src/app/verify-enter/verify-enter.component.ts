import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthorizationService } from '../services/authorization.service';
import AuthHelper from '../utils/authHelper';

@Component({
  selector: 'app-verify-enter',
  templateUrl: './verify-enter.component.html',
  styleUrls: ['./verify-enter.component.css']
})
export class VerifyEnterComponent implements OnInit {

  email : string = '';
  code : string = '';

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private authService: AuthorizationService,
    ) {

   }

  ngOnInit(): void {
    // Note: Below 'queryParams' can be replaced with 'params' depending on your requirements
    this.activatedRoute.queryParams.subscribe(
      (params: any) => {
        this.email = params['email'];
        this.code = params['code'];
    });
    console.log(this.email);
    console.log(this.code);

    fetch(`https://localhost:44381/api/codes/verifyenteruser?email=${this.email}&code=${this.code}`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          console.log(this.authService.getLogCondition());
          AuthHelper.saveAuth(this.email, "test");
          this.authService.toggleLogCondition();

          this.router.navigate(['']);
        } else {
          alert("Enter error!");
        }
      })
      .catch((ex) => {
        alert(ex);
      });

  }

}
