import { Component, OnInit } from '@angular/core';
import { TempUser } from 'src/app/models/UserData/tempuser.item';

import AuthHelper from '../../../utils/authHelper';

@Component({
  selector: 'app-temp-users-list',
  templateUrl: './temp-users-list.component.html',
  styleUrls: ['./temp-users-list.component.css']
})
export class TempUsersListComponent implements OnInit {

  users: TempUser[] | null = null;
  user: string | null = null;
  checkedUser: number | null = null;

  constructor() {}

  addUser(): void {
    let user = {
      name: this.user,
    };

    fetch('https://localhost:44381/api/users/adduser', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(user),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getUsers();
        } else {
          alert('Adding error!');
        }
        this.user = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editUser(): void {
    let user = {
      id: this.checkedUser,
      name: this.user,
    };

    fetch('https://localhost:44381/api/users/edituser', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(user),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getUsers();
        } else {
          alert('Editing error!');
        }
        this.user = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteUser(): void {
    let user = {
      id: this.checkedUser,
      name: this.user,
    };

    fetch('https://localhost:44381/api/users/deleteuser', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(user),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getUsers();
        } else {
          alert('Editing error!');
        }
        this.user = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getUsers(): void {
    fetch('https://localhost:44381/api/users/getusers', {
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

  setUser(id: number | null, user: string): void {
    this.checkedUser = id;
    this.user = user;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getUsers();
  }

}
