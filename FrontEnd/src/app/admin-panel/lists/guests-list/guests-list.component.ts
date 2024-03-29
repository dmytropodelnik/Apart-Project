import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Guest } from 'src/app/models/UserData/guest.item';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-guests-list',
  templateUrl: './guests-list.component.html',
  styleUrls: ['./guests-list.component.css'],
})
export class GuestsListComponent implements OnInit {
  users: Guest[] | null = null;
  user: Guest;
  searchUser: string = '';
  checkedUser: number | null = null;

  page: number = 1;
  pageSize: number = 10;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private adminContentService: AdminContentService,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {
    this.user = new Guest();
  }

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  search(): void {
    fetch(
      'https://apartmain.azurewebsites.net/api/guests/search?user=' +
        this.searchUser,
      {
        method: 'GET',
        headers: {
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
          this.showAlert('Search error!');
        }
        this.searchUser = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  addUser(): void {
    let user = {
      name: this.user,
    };

    fetch('https://apartmain.azurewebsites.net/api/guests/adduser', {
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
        this.user = new Guest();
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  editUser(): void {
    let user = {
      id: this.checkedUser,
      name: this.user,
    };

    fetch('https://apartmain.azurewebsites.net/api/guests/edituser', {
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
          this.showAlert('Editing error!');
        }
        this.user = new Guest();
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  disableButtons(): void {
    document.getElementById('editButton')?.setAttribute('disabled', 'disabled');
    document
      .getElementById('deleteButton')
      ?.setAttribute('disabled', 'disabled');
  }

  deleteUser(): void {
    let user = {
      id: this.checkedUser,
      name: this.user,
    };

    fetch('https://apartmain.azurewebsites.net/api/guests/deleteuser', {
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
          this.showAlert('Editing error!');
        }
        this.user = new Guest();
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  getUsers(): void {
    fetch(
      `https://apartmain.azurewebsites.net/api/guests/getusers?page=${this.page}&pageSize=${this.pageSize}`,
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
        this.showAlert(ex);
      });
  }

  collectElements(users: Guest[]): void {
    for (let item of users) {
      this.users?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(
      `https://apartmain.azurewebsites.net/api/guests/getusers?page=${this.page}&pageSize=${this.pageSize}`,
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
        this.showAlert(ex);
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
