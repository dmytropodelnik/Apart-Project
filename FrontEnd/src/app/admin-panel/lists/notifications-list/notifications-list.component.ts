import { Component, OnInit } from '@angular/core';

import Notification from '../../../models/notification.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-notifications-list',
  templateUrl: './notifications-list.component.html',
  styleUrls: ['./notifications-list.component.css']
})
export class NotificationsListComponent implements OnInit {

  notifications: Notification[] | null = null;
  notification: string | null = null;
  checkedNotification: number | null = null;

  constructor() {}

  addNotification(): void {
    let notification = {
      name: this.notification,
    };

    fetch('https://localhost:44381/api/notifications/addnotification', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
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
        alert(ex);
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
        Authorization: 'Bearer ' + AuthHelper.getToken(),
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
        alert(ex);
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
        Authorization: 'Bearer ' + AuthHelper.getToken(),
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
        alert(ex);
      });
  }

  getNotifications(): void {
    fetch('https://localhost:44381/api/notifications/getnotifications', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.notifications = data.notifications;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
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
