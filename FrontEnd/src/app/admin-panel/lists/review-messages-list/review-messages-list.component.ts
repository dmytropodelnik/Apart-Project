import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ReviewMessage } from 'src/app/models/Review/reviewmessage.item';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-review-messages-list',
  templateUrl: './review-messages-list.component.html',
  styleUrls: ['./review-messages-list.component.css'],
})
export class ReviewMessagesListComponent implements OnInit {
  reviewMessages: ReviewMessage[] | null = null;
  message: ReviewMessage | null = null;
  checkedMessage: number | null = null;

  page: number = 1;
  pageSize: number = 15;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private adminContentService: AdminContentService,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
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
          this.showAlert('Adding error!');
        }
        this.message = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  editMessage(): void {
    let message = {
      id: this.checkedMessage,
      name: this.message,
    };

    fetch(
      'https://apartmain.azurewebsites.net/api/reviewmessages/editmessage',
      {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
        body: JSON.stringify(message),
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getMessages();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.message = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  deleteMessage(): void {
    let message = {
      id: this.checkedMessage,
      name: this.message,
    };

    fetch(
      'https://apartmain.azurewebsites.net/api/reviewmessages/deletemessage',
      {
        method: 'DELETE',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
        body: JSON.stringify(message),
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getMessages();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.message = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  getMessages(): void {
    fetch(
      `https://apartmain.azurewebsites.net/api/reviewmessages/getmessages?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.reviewMessages = data.messages;
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  collectElements(messages: ReviewMessage[]): void {
    for (let item of messages) {
      this.reviewMessages?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(
      `https://apartmain.azurewebsites.net/api/reviewmessages/getmessages?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.collectElements(data.messages);
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
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
