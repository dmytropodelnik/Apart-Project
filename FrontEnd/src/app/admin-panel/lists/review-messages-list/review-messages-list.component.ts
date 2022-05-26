import { Component, OnInit } from '@angular/core';
import { ReviewMessage } from 'src/app/models/Review/reviewmessage.item';
import { AdminContentService } from 'src/app/services/admin-content.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-review-messages-list',
  templateUrl: './review-messages-list.component.html',
  styleUrls: ['./review-messages-list.component.css']
})
export class ReviewMessagesListComponent implements OnInit {

  reviewMessages: ReviewMessage[] | null = null;
  message: ReviewMessage | null = null;
  checkedMessage: number | null = null;

  page: number = 1;
  pageSize: number = 15;

  constructor(
    private adminContentService: AdminContentService
  ) {

  }

  addMessage(): void {
    let message = {
      name: this.message,
    };

    fetch('https://apartmain.azurewebsites.net/api/reviewmessages/addmessage', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
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
        this.message = null;
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

    fetch('https://apartmain.azurewebsites.net/api/reviewmessages/editmessage', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
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
        this.message = null;
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

    fetch('https://apartmain.azurewebsites.net/api/reviewmessages/deletemessage', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
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
        this.message = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getMessages(): void {
    fetch(`https://apartmain.azurewebsites.net/api/reviewmessages/getmessages?page=${this.page}&pageSize=${this.pageSize}`, {
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
          this.reviewMessages = data.messages;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  collectElements(messages: ReviewMessage[]): void {
    for (let item of messages) {
      this.reviewMessages?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(`https://apartmain.azurewebsites.net/api/reviewmessages/getmessages?page=${this.page}&pageSize=${this.pageSize}`, {
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
          this.collectElements(data.messages);
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setMessage(message: ReviewMessage): void {
    this.checkedMessage = message.id;
    this.message = message;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getMessages();
  }

}
