import { Component, OnInit } from '@angular/core';
import { TempUser } from 'src/app/models/UserData/tempuser.item';

@Component({
  selector: 'app-temp-users-list',
  templateUrl: './temp-users-list.component.html',
  styleUrls: ['./temp-users-list.component.css']
})
export class TempUsersListComponent implements OnInit {

  users: TempUser[] | null = null;

  constructor() { }

  ngOnInit(): void {
    fetch('https://localhost:44381/api/countries/getcountries', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.users = data.users;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

}
