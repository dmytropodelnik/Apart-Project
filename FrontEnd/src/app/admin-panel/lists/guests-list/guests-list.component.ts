import { Component, OnInit } from '@angular/core';
import { Guest } from 'src/app/models/UserData/guest.item';
import { AdminContentService } from 'src/app/services/admin-content.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-guests-list',
  templateUrl: './guests-list.component.html',
  styleUrls: ['./guests-list.component.css']
})
export class GuestsListComponent implements OnInit {

  users: Guest[] | null = null;
  user: Guest;
  searchUser: string = '';
  checkedUser: number | null = null;

  page: number = 1;
  pageSize: number = 10;

  constructor(
    private adminContentService: AdminContentService
  ) {
    this.user = new Guest();
  }

  search(): void {
    fetch('https://localhost:44381/api/guests/search?user=' + this.searchUser, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
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

    fetch('https://localhost:44381/api/guests/adduser', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
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
        this.user = new Guest();
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

    fetch('https://localhost:44381/api/guests/edituser', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
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
        this.user = new Guest();
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

    fetch('https://localhost:44381/api/guests/deleteuser', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
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
        this.user = new Guest();
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getUsers(): void {
    fetch(`https://localhost:44381/api/guests/getusers?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
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

  collectElements(users: Guest[]): void {
    for (let item of users) {
      this.users?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(`https://localhost:44381/api/guests/getusers?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.users);
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setUser(user: Guest): void {
    this.checkedUser = user.id;
    this.user = user;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getUsers();
  }

}