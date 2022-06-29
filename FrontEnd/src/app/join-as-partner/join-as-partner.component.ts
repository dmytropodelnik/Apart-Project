import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';

import AuthHelper from '../utils/authHelper';
import ImageHelper from '../utils/imageHelper';

import { AuthorizationService } from '../services/authorization.service';
import { MainDataService } from '../services/main-data.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-join-as-partner',
  templateUrl: './join-as-partner.component.html',
  styleUrls: ['./join-as-partner.component.css'],
})
export class JoinAsPartnerComponent implements OnInit {
  authHelper: any = AuthHelper;
  imageHelper: any = ImageHelper;
  login = false;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    public authService: AuthorizationService,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  userSignOut(): void {
    let model = {
      username: AuthHelper.getLogin(),
      accessToken: AuthHelper.getToken(),
    };

    fetch('https://localhost:44381/api/users/signoutuser', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
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
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  ngOnInit(): void {}
}
