import { Component, OnInit } from '@angular/core';
import AuthHelper from "../utils/authHelper"
import { AuthorizationService } from '../services/authorization.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {

  username: string;
  password: string;

  constructor(private authService: AuthorizationService) {
    this.username = "";
    this.password = "";
  }

  userCheck($event : any): void {

    // let user = {
    //   username: this.username,
    //   password: this.password
    // };

    // fetch('https://localhost:44341/token', {
    //             method: 'POST',
    //             headers: {
    //                 'Content-Type': 'application/json; charset=utf-8'
    //             },
    //             body: JSON.stringify(user)
    //         }).then((response) => {
    //             if (response.ok) {
    //                 return response.json();
    //             } else {
    //                 console.log(this.authService.getLogCondition());
    //                 console.log(this.username);
    //                 console.log(this.password);
    //                 alert("Error authorization");
    //                 return "Error";
    //             }
    //         }).then((data) => {
    //             AuthHelper.saveAuth(user.username, data.access_token);
    //             this.authService.toggleLogCondition();
    //             console.log(this.authService.getLogCondition());
    //             alert("You have successfully authenticated!");
    //             //this.router.navigate(['/welcome']);
    //         }).catch((ex) => {
    //             alert(ex);
    //         });
  }

  ngOnInit(): void {
  }

}
