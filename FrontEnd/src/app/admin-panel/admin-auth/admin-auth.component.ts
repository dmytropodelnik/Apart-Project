import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-admin-auth',
  templateUrl: './admin-auth.component.html',
  styleUrls: ['./admin-auth.component.css']
})
export class AdminAuthComponent implements OnInit {

  login: string | undefined;
  password: string | undefined;

  constructor() { }

  loginAdmin() {
    let user = {
      login: this.login,
      password: this.password,
    };

    fetch('https://localhost:44381/api/admin/login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
       },
      body: JSON.stringify(user),
    })
      .then((r) => r.text())
      .then((data) => {
        alert(data);
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  ngOnInit(): void {
  }

}
