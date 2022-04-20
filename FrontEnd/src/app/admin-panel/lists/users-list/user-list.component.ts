import { Component, OnInit } from '@angular/core';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { User } from '../../../models/UserData/user.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-users-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  users: User[] | null = null;
  user: User;
  searchUser: string = '';
  checkedUser: number | null = null;
  confirmPass: string = '';

  page: number = 1;
  pageSize: number = 10;

  constructor(
    private adminContentService: AdminContentService
  ) {
    this.user = new User();
  }

  search(): void {
    fetch('https://apartmain.azurewebsites.net/api/users/search?user=' + this.searchUser, {
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
      email: this.user.email,
      passwordHash: this.user.passwordHash,
      roleId: this.user.role.id,
    };

    fetch('https://apartmain.azurewebsites.net/api/users/adduser', {
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
        this.user = new User();
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

    fetch('https://apartmain.azurewebsites.net/api/users/edituser', {
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
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.user = new User();
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

    fetch('https://apartmain.azurewebsites.net/api/users/deleteuser', {
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
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.user = new User();
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getUsers(): void {
    fetch(`https://apartmain.azurewebsites.net/api/users/getusers?page=${this.page}&pageSize=${this.pageSize}`, {
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

  getUserRoles(): void {
    fetch('https://apartmain.azurewebsites.net/api/roles/getroles', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          let rolesAdd = document.getElementById('newRole');
          let newOption;
          let counter = 1;
          for (let role of data.roles) {
            newOption = new Option(role.name, counter.toString());
            rolesAdd?.append(newOption);
            counter++;
          }

          let rolesEdit = document.getElementById('editRole');
          counter = 1;
          for (let role of data.roles) {
            newOption = new Option(role.name, counter.toString());
            rolesEdit?.append(newOption);
            counter++;
          }

          this.user.role.id = 1;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  collectElements(users: User[]): void {
    for (let item of users) {
      this.users?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(`https://apartmain.azurewebsites.net/api/users/getusers?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
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

  setUser(user: User): void {
    this.checkedUser = user.id;
    this.user = user;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getUsers();
    this.getUserRoles();
  }

}
