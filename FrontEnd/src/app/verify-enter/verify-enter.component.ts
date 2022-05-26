import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { RepositoryEnum } from '../enums/repositoryenum.item';
import { AuthorizationService } from '../services/authorization.service';

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
  isToResetPassword: boolean = false;
  isToChangeEmail: boolean = false;
  isToDeleteCode: boolean = false;
  isToDeleteUser: boolean = false;
  isToSubscribeUser: boolean = false;

  repositoryEnum: RepositoryEnum = RepositoryEnum.Enter;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private authService: AuthorizationService
  ) {}

  async verifyEnterUser(): Promise<void> {
    fetch(
      `https://apartmain.azurewebsites.net/api/codes/verifyenteruser?email=${this.email}&code=${this.code}&confidant=true`,
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
        alert(ex);
        this.router.navigate(['']);
      });
  }

  async authorize(): Promise<void> {
    let user = {
      email: this.email,
      code: this.code,
      isToDeleteCode: this.isToDeleteCode,
      repository: this.repositoryEnum,
    };

    fetch('https://apartmain.azurewebsites.net/token', {
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
            this.router.navigate(['']);
          }
        } else {
          alert('Token fetching error!');
          this.router.navigate(['']);
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  resetPassword(): void {
    fetch(`https://apartmain.azurewebsites.net/api/codes/verifypasswordreset?email=${this.email}&code=${this.code}&confidant=true`, {
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
        alert(ex);
        this.router.navigate(['']);
      });
  }

  async saveEmail(): Promise<void> {
    let user = {
      email: this.oldEmail,
      newEmail: this.email,
    };

    await fetch('https://apartmain.azurewebsites.net/api/userdataeditor/editemail', {
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
        } else {
          alert('Save email error!');
        }
        this.router.navigate(['']);
      })
      .catch((ex) => {
        alert(ex);
        this.router.navigate(['']);
      });
  }

  deleteUserEventually(): void {
    fetch(`https://apartmain.azurewebsites.net/api/users/deleteuser?email=${this.email}`, {
        method: 'DELETE',
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
          alert("Your account has been successfully deleted!");
        } else {
          alert('Delete user error!');
        }
        this.router.navigate(['']);
      })
      .catch((ex) => {
        alert(ex);
        this.router.navigate(['']);
      });
  }

  deleteUser(): void {
    fetch(`https://apartmain.azurewebsites.net/api/codes/verifyuserdeletion?email=${this.email}&code=${this.code}`, {
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
        alert(ex);
        this.router.navigate(['']);
      });
  }

  changeEmail(): void {
    fetch(
      `https://apartmain.azurewebsites.net/api/codes/verifyemailchanging?email=${this.email}&code=${this.code}&confidant=true`, {
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
        alert(ex);
        this.router.navigate(['']);
      });
  }

  subscribeUser(): void {
    fetch(
      `https://localhost:44381/api/codes/verifyusersubscription?email=${this.email}&code=${this.code}`, {
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
        alert(ex);
        this.router.navigate(['']);
      });
  }

  addUserSubscription(): void {
    fetch(
      'https://localhost:44381/api/deals/addsubscriber?email=' +
        this.email,
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
