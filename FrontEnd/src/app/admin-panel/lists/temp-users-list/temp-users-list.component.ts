import { Component, OnInit } from '@angular/core';
import { TempUser } from 'src/app/models/UserData/tempuser.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-temp-users-list',
  templateUrl: './temp-users-list.component.html',
  styleUrls: ['./temp-users-list.component.css']
})
export class TempUsersListComponent implements OnInit {

  users: TempUser[] | null = null;
  user: TempUser | null = null;
  searchUser: string = '';
  checkedUser: number | null = null;

  constructor() {}

  search(): void {
    fetch('https://localhost:44381/api/tempusers/search?user=' + this.searchUser, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.users = data.users;
        } else {
          alert('Search error!');
        }
        this.searchUser = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addUser(): void {
    let user = {
      name: this.user,
    };

    fetch('https://localhost:44381/api/tempusers/adduser', {
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
        this.user = null;
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

    fetch('https://localhost:44381/api/tempusers/edituser', {
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
          this.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.user = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  disableButtons(): void {
    document.getElementById('editButton')?.setAttribute('disabled', 'disabled');
    document.getElementById('deleteButton')?.setAttribute('disabled', 'disabled');
  }

  deleteUser(): void {
    let user = {
      id: this.checkedUser,
      name: this.user,
    };

    fetch('https://localhost:44381/api/tempusers/deleteuser', {
      method: 'DELETE',
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
          this.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.user = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getUsers(): void {
    fetch('https://localhost:44381/api/tempusers/getusers', {
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

  setUser(user: TempUser): void {
    this.checkedUser = user.id;
    this.user = user;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getUsers();
  }

}
