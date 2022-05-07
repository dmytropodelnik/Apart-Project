import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import AuthHelper from '../utils/authHelper';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css'],
})
export class FooterComponent implements OnInit {
  public isCollapsed = true;
  email: string = '';
  emailPattern = '^[a-z0-9._%+-]+@[a-z0-9.-]+.[a-z]{2,4}$';

  constructor() {}

  fetchRequest() {
    if (!this.email.match('^[a-z0-9._%+-]+@[a-z0-9.-]+.[a-z]{2,4}$')) {
      alert('Incorrect email pattern!');
      return;
    }

    fetch(
      'https://localhost:44381/api/deals/sendbestdealsletter?email=' +
        this.email,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((r) => r.json())
      .then((r) => {
        if (r.code === 200) {
          alert('A letter has been successfully sent to your email address!');
        } else {
          alert('Error!');
        }
      })
      .catch((err) => {
        alert(err);
      });
  }

  ngOnInit(): void {}
}
