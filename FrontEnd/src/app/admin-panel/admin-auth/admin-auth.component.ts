import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-auth',
  templateUrl: './admin-auth.component.html',
  styleUrls: ['./admin-auth.component.css']
})
export class AdminAuthComponent implements OnInit {

  login: string | undefined;
  password: string | undefined;

  constructor(
    private router: Router
    ) {

   }

  loginAdmin() : void {
    let user = {
      email: this.login,
      password: this.password,
    };

    fetch('https://localhost:44381/api/admin/login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
       },
      body: JSON.stringify(user),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.router.navigate(['/admin']);
        } else {
          alert("Incorrect data");
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  ngOnInit(): void {
  }

}
