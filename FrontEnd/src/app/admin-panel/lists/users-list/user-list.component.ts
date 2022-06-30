import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';
import { User } from '../../../models/UserData/user.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-users-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css'],
})
export class UserListComponent implements OnInit {
  users: User[] | null = null;
  user: User;
  searchUser: string = '';
  checkedUser: number | null = null;
  confirmPass: string = '';

  page: number = 1;
  pageSize: number = 10;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private adminContentService: AdminContentService,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {
    this.user = new User();
  }

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  search(): void {
    fetch('https://localhost:44381/api/users/search?user=' + this.searchUser, {
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
          this.showAlert('Search error!');
        }
        this.searchUser = '';
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  addUser(): void {
    let user = {
      email: this.user.email,
      passwordHash: this.user.passwordHash,
      roleId: this.user.role.id,
    };

    fetch('https://localhost:44381/api/users/adduser', {
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
          this.showAlert('Adding error!');
        }
        this.user = new User();
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
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
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(user),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getUsers();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.user = new User();
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  deleteUser(): void {
    fetch('https://localhost:44381/api/users/deluser?id=' + this.checkedUser, {
      method: 'DELETE',
      headers: {
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getUsers();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.user = new User();
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  getUsers(): void {
    fetch(
      `https://localhost:44381/api/users/getusers?page=${this.page}&pageSize=${this.pageSize}`,
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
      .then((data) => {
        if (data.code === 200) {
          this.users = data.users;
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  getUserRoles(): void {
    fetch('https://localhost:44381/api/roles/getroles', {
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
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  collectElements(users: User[]): void {
    for (let item of users) {
      this.users?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(
      `https://localhost:44381/api/users/getusers?page=${this.page}&pageSize=${this.pageSize}`,
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
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.users);
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
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
