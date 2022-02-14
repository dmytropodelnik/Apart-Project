import { Component, OnInit } from '@angular/core';
import { ReviewMessage } from 'src/app/models/Review/reviewmessage.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-review-messages-list',
  templateUrl: './review-messages-list.component.html',
  styleUrls: ['./review-messages-list.component.css']
})
export class ReviewMessagesListComponent implements OnInit {

  reviewMessages: ReviewMessage[] | null = null;
  message: string | null = null;
  checkedMessage: number | null = null;

  constructor() {}

  addMessage(): void {
    let message = {
      name: this.message,
    };

    fetch('https://localhost:44381/api/messages/addmessage', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(message),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getMessages();
        } else {
          alert('Adding error!');
        }
        this.message = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editMessage(): void {
    let message = {
      id: this.checkedMessage,
      name: this.message,
    };

    fetch('https://localhost:44381/api/messages/editmessage', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(message),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getMessages();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.message = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteMessage(): void {
    let message = {
      id: this.checkedMessage,
      name: this.message,
    };

    fetch('https://localhost:44381/api/messages/deletemessage', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(message),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getMessages();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.message = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getMessages(): void {
    fetch('https://localhost:44381/api/messages/getmessages', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.reviewMessages = data.messages;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setMessage(id: number | null, message: string): void {
    this.checkedMessage = id;
    this.message = message;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getMessages();
  }

}
