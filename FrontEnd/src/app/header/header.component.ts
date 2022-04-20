import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { User } from '../models/UserData/user.item';

import AuthHelper from '../utils/authHelper';
import ImageHelper from '../utils/imageHelper';

import { AuthorizationService } from '../services/authorization.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  public isCollapsed = false;
  user: User | null = null;
  authHelper: any = AuthHelper;
  displayMonths = 2;
  navigation = 'arrows';
  showWeekNumbers = false;
  outsideDays = 'hidden';
  imageHelper: any = ImageHelper;
  isActive1 = true;

  @ViewChild('content') content!: TemplateRef<any>;
  @ViewChild('content1') content1!: TemplateRef<any>;

  constructor(
    config: NgbModalConfig,
    private modalService: NgbModal,
    public authService: AuthorizationService
  ) {}

  getToken(): string {
    return AuthHelper.getToken();
  }

  userSignOut(): void {
    let model = {
      username: AuthHelper.getLogin(),
      accessToken: AuthHelper.getToken(),
    };

    fetch('https://apartmain.azurewebsites.net/api/users/signoutuser', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(model),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.authService.setLogCondition(false);
          AuthHelper.clearAuth();
        } else {
          alert('Logout error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  ngOnInit(): void {}

  open() {
    this.modalService.open(this.content, { size: 'lg', centered: true });
  }

  open1() {
    this.modalService.open(this.content1, { size: 'lg', centered: true });
  }
}
