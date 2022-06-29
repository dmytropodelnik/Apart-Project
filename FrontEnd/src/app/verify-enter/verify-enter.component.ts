import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { RepositoryEnum } from '../enums/repositoryenum.item';
import { AuthorizationService } from '../services/authorization.service';
import { MainDataService } from '../services/main-data.service';

import AuthHelper from '../utils/authHelper';

@Component({
  selector: 'app-verify-enter',
  templateUrl: './verify-enter.component.html',
  styleUrls: ['./verify-enter.component.css'],
})
export class VerifyEnterComponent implements OnInit {
  email: string = '';
  oldEmail: string = '';
  code: string = '';
  letterMessage: string = '';

  isToResetPassword: boolean = false;
  isToChangeEmail: boolean = false;
  isToDeleteCode: boolean = false;
  isToDeleteUser: boolean = false;
  isToSubscribeUser: boolean = false;
  letterAction: boolean = false;

  repositoryEnum: RepositoryEnum = RepositoryEnum.Enter;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private authService: AuthorizationService,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  async verifyEnterUser(): Promise<void> {
    fetch(
      `https://localhost:44381/api/codes/verifyenteruser?email=${this.email}&code=${this.code}&confidant=true`,
      {
        method: 'GET',
      }
    )
      .then((r) => r.json())
      .then(async (data) => {
        if (data.code === 200) {
          this.repositoryEnum = RepositoryEnum.Enter;
          await this.authorize();
        } else {
          alert('Enter error!');
          this.router.navigate(['']);
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
        this.router.navigate(['']);
      });
  }

  sendInfoLetter(): void {
    fetch(
      `https://localhost:44381/api/notifications/sendnotification?email=${this.email}&message=${this.letterMessage}&action=${this.letterAction}`,
      {
        method: 'GET',
      }
    )
      .then((r) => r.json())
      .then(async (data) => {
        if (data.code === 200) {
        } else {
          alert(data.message);
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  async authorize(): Promise<void> {
    let user = {
      email: this.email,
      code: this.code,
      isToDeleteCode: this.isToDeleteCode,
      repository: this.repositoryEnum,
    };

    fetch('https://localhost:44381/token', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
      },
      body: JSON.stringify(user),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.authService.setTokenKey(response.encodedJwt);
          AuthHelper.saveAuth(response.email, response.encodedJwt);
          this.authService.toggleLogCondition();

          alert('You have successfully authenticated!');

          if (this.isToChangeEmail) {
            this.router.navigate(['/mysettings']);
          } else if (this.isToResetPassword) {
            alert('Redirecting to reseting page!');
            this.router.navigate(['/resetpassword']);
          } else {
            this.letterMessage = `You have successfully entered on Apartstep.fun with ${this.email}!`;
            this.letterAction = true;
            this.sendInfoLetter();
            this.router.navigate(['']);
          }
        } else {
          alert('Token fetching error!');
          this.router.navigate(['']);
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  resetPassword(): void {
    fetch(
      `https://localhost:44381/api/codes/verifypasswordreset?email=${this.email}&code=${this.code}&confidant=true`,
      {
        method: 'GET',
      }
    )
      .then((r) => r.json())
      .then(async (data) => {
        if (data.code === 200) {
          this.authService.setResetPasswordCondition(true);
          this.repositoryEnum = RepositoryEnum.ResetPassword;
          await this.authorize();
        } else {
          alert('Enter error!');
          this.router.navigate(['']);
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
        this.router.navigate(['']);
      });
  }

  async saveEmail(): Promise<void> {
    let user = {
      email: this.oldEmail,
      newEmail: this.email,
    };

    await fetch('https://localhost:44381/api/userdataeditor/editemail', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(user),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.email = response.resUser.email;
          alert('You have successfully changed your email!');
          this.letterMessage = `You have successfully changed your email on Apartstep.fun to ${this.email}!`;
          this.letterAction = false;
          this.sendInfoLetter();
        } else {
          alert('Save email error!');
        }
        this.router.navigate(['']);
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
        this.router.navigate(['']);
      });
  }

  deleteUserEventually(): void {
    fetch(`https://localhost:44381/api/users/deleteuser?email=${this.email}`, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          alert('Your account has been successfully deleted!');
          this.letterMessage = `You have successfully deleted your account on Apartstep.fun with ${this.email}!`;
          this.letterAction = true;
          this.sendInfoLetter();
        } else {
          alert('Delete user error!');
        }
        this.router.navigate(['']);
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
        this.router.navigate(['']);
      });
  }

  deleteUser(): void {
    fetch(
      `https://localhost:44381/api/codes/verifyuserdeletion?email=${this.email}&code=${this.code}`,
      {
        method: 'GET',
      }
    )
      .then((r) => r.json())
      .then(async (data) => {
        if (data.code === 200) {
          this.deleteUserEventually();
        } else {
          alert('Verify user deletion error!');
          this.router.navigate(['']);
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
        this.router.navigate(['']);
      });
  }

  changeEmail(): void {
    fetch(
      `https://localhost:44381/api/codes/verifyemailchanging?email=${this.email}&code=${this.code}&confidant=true`,
      {
        method: 'GET',
      }
    )
      .then((r) => r.json())
      .then(async (data) => {
        if (data.code === 200) {
          await this.saveEmail();
          this.repositoryEnum = RepositoryEnum.ChangingEmail;
          await this.authorize();
        } else {
          alert('Verify email changing error!');
          this.router.navigate(['']);
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
        this.router.navigate(['']);
      });
  }

  subscribeUser(): void {
    fetch(
      `https://localhost:44381/api/codes/verifyusersubscription?email=${this.email}&code=${this.code}`,
      {
        method: 'GET',
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.addUserSubscription();
        } else {
          alert('Verifying email to subscribe to our news error!');
          this.router.navigate(['']);
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
        this.router.navigate(['']);
      });
  }

  addUserSubscription(): void {
    fetch(
      'https://localhost:44381/api/deals/addsubscriber?email=' + this.email,
      {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((r) => r.json())
      .then((r) => {
        if (r.code === 200) {
          alert('You have successfully subscribed to our new deals!');
          this.letterMessage = `You have successfully subscribed to Apartstep.fun new deals!`;
          this.letterAction = true;
          this.sendInfoLetter();
        } else {
          alert('Add deals subscriber error!');
        }
        this.router.navigate(['']);
      })
      .catch((err) => {
        alert(err);
      });
  }

  async ngOnInit(): Promise<void> {
    // Note: Below 'queryParams' can be replaced with 'params' depending on your requirements
    this.activatedRoute.queryParams.subscribe(async (params: any) => {
      if (!params['email'] || !params['code']) {
        this.router.navigate(['']);
        return;
      }

      this.email = params['email'];
      this.oldEmail = params['oldEmail'];
      this.code = params['code'];
      this.isToResetPassword = params['resetPassword'];
      this.isToChangeEmail = params['changeEmail'];
      this.isToDeleteCode = params['isToDeleteCode'];
      this.isToDeleteUser = params['isToDeleteUser'];
      this.isToSubscribeUser = params['subscribeNews'];

      if (this.isToChangeEmail) {
        this.repositoryEnum = RepositoryEnum.ChangingEmail;
        this.changeEmail();
      } else if (this.isToResetPassword) {
        this.repositoryEnum = RepositoryEnum.ResetPassword;
        this.resetPassword();
      } else if (this.isToDeleteUser) {
        this.repositoryEnum = RepositoryEnum.UserDeletion;
        this.deleteUser();
      } else if (this.isToSubscribeUser) {
        this.repositoryEnum = RepositoryEnum.UserSubscription;
        this.subscribeUser();
      } else {
        await this.verifyEnterUser();
      }
    });
  }
}
