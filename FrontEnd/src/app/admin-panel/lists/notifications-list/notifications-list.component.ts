import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';

import Notification from '../../../models/notification.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-notifications-list',
  templateUrl: './notifications-list.component.html',
  styleUrls: ['./notifications-list.component.css'],
})
export class NotificationsListComponent implements OnInit {
  notifications: Notification[] | null = null;
  notification: string | null = null;
  searchNotification: string = '';
  checkedNotification: number | null = null;

  page: number = 1;
  pageSize: number = 15;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private adminContentService: AdminContentService,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  search(): void {
    fetch(
      'https://localhost:44381/api/notifications/search?notification=' +
        this.searchNotification,
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
          this.notifications = data.notifications;
        } else {
          alert('Search error!');
        }
        this.searchNotification = '';
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  addNotification(): void {
    let notification = {
      name: this.notification,
    };

    fetch('https://localhost:44381/api/notifications/addnotification', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(notification),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getNotifications();
        } else {
          alert('Adding error!');
        }
        this.notification = '';
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  editNotification(): void {
    let notification = {
      id: this.checkedNotification,
      name: this.notification,
    };

    fetch('https://localhost:44381/api/notifications/editnotification', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(notification),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getNotifications();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.notification = '';
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  deleteNotification(): void {
    let notification = {
      id: this.checkedNotification,
      name: this.notification,
    };

    fetch('https://localhost:44381/api/notifications/deletenotification', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(notification),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getNotifications();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.notification = '';
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  getNotifications(): void {
    fetch(
      `https://localhost:44381/api/notifications/getnotifications?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.notifications = data.notifications;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  collectElements(notifications: Notification[]): void {
    for (let item of notifications) {
      this.notifications?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(
      `https://localhost:44381/api/notifications/getnotifications?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.collectElements(data.notifications);
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  setNotification(id: number | null, notif: string): void {
    this.checkedNotification = id;
    this.notification = notif;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getNotifications();
  }
}
