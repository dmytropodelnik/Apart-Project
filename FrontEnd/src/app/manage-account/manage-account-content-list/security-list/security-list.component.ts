import {
  Component,
  OnInit,
  Output,
  EventEmitter,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { Router } from '@angular/router';

import AuthHelper from '../../../utils/authHelper';
import ImageHelper from '../../../utils/imageHelper';

import { AuthorizationService } from '../../../services/authorization.service';
import { UserData } from 'src/app/view-models/userdata.item';
import { MainDataService } from 'src/app/services/main-data.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-security-list',
  templateUrl: './security-list.component.html',
  styleUrls: ['./security-list.component.css'],
})
export class SecurityListComponent implements OnInit {
  @Output() onChanged = new EventEmitter<string>();
  changeEmail(setting: string) {
    this.onChanged.emit(setting);
  }

  isEditing: boolean[] = [];
  isDisabled: boolean[] = [];

  authHelper: any = AuthHelper;

  isDeleteRequested: boolean = false;
  isEmailSent: boolean = false;

  deleteReason: number = 0;

  user: UserData = new UserData();

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    public authService: AuthorizationService,
    private router: Router,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  setDeleteReason(value: number) {
    this.deleteReason = value;
  }

  setCondition(id: number): void {
    this.isEditing[id] = !this.isEditing[id];
  }

  editButtonClick(id: number): void {
    this.setCondition(id);
    this.setConditionEditButtons(id, true);
  }

  saveButtonClick(id: number): void {
    this.setCondition(id);
    this.setConditionEditButtons(id, false);
  }

  cancelButtonClick(id: number): void {
    this.setCondition(id);
    this.setConditionEditButtons(id, false);
    this.deleteReason = 0;
  }

  setConditionEditButtons(id: number, value: boolean): void {
    for (let i = 0; i < this.isDisabled.length; i++) {
      if (i !== id) {
        this.isDisabled[i] = value;
      }
    }
  }

  sendResetPasswordEmail(id: number): void {
    fetch(
      `https://apartmain.azurewebsites.net/api/codes/generateresetcode?email=` +
        AuthHelper.getLogin(),
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
          this.isEmailSent = true;
        } else {
          this.showAlert('Error generating reset link');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  unsubscribeMails(id: number): void {
    this.cancelButtonClick(id);

    this.showAlert('You have successfully unsubscribed from mail letters!');
  }

  deleteAccount(id: number): void {
    fetch(
      `https://apartmain.azurewebsites.net/api/codes/generatedeleteusercode?email=` +
        AuthHelper.getLogin(),
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
          this.isDeleteRequested = true;
          this.cancelButtonClick(id);
        } else {
          this.showAlert('Error generating delete user code');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  initializeBoolArray(): void {
    for (let i = 0; i < 2; i++) {
      this.isDisabled[i] = false;
    }
  }

  ngOnInit(): void {
    this.initializeBoolArray();
  }
}
