import {
  Component,
  OnInit,
  OnDestroy,
  HostListener,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MainDataService } from 'src/app/services/main-data.service';

import { AuthorizationService } from '../../services/authorization.service';

import AuthHelper from '../../utils/authHelper';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css'],
})
export class AdminPanelComponent implements OnInit, OnDestroy {
  currentYear: number = new Date().getFullYear();
  content: string | undefined;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private router: Router,
    private authService: AuthorizationService,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  setContent(newContent: string) {
    this.content = newContent;
  }

  async userSignOut(): Promise<void> {
    let model = {
      userName: AuthHelper.getLogin(),
      accessToken: AuthHelper.getToken(),
    };

    fetch('https://apartmain.azurewebsites.net/api/users/signoutuser', {
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
          this.authService.setIsAdmin(false);
          AuthHelper.clearAuth();

          this.showAlert('Success logout');
          document.location.href = 'https://www.apartstep.fun';
        } else {
          this.showAlert('Logout error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  ngOnInit(): void {
    // if (!this.authService.getIsAdmin()      ||
    //     !this.authService.getLogCondition()) {
    //       this.router.navigate(['']);
    // }
  }

  @HostListener('window:beforeunload')
  async ngOnDestroy() {
    this.userSignOut();
  }
}
