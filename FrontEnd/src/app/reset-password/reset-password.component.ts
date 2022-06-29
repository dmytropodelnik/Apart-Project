import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';

import AuthHelper from '../utils/authHelper';
import { AuthorizationService } from '../services/authorization.service';
import { Router, ActivatedRoute } from '@angular/router';

import {
  FormBuilder,
  FormGroup,
  Validators,
  AbstractControl,
} from '@angular/forms';
import { MainDataService } from '../services/main-data.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css'],
})
export class ResetPasswordComponent implements OnInit {
  passwordForm: FormGroup;

  password: string = '';
  confirmPassword: string = '';

  email: string = '';
  code: string = '';

  letterMessage: string = '';
  letterAction: boolean = false;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private authService: AuthorizationService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {
    this.passwordForm = this.formBuilder.group(
      {
        password: ['', [Validators.required, Validators.minLength(8)]],
        confirmPassword: ['', [Validators.required]],
      },
      {
        validator: this.MustMatch('password', 'confirmPassword'),
      }
    );
  }

  get f1() {
    return this.passwordForm.controls;
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

  resetPassword(): void {
    let user = {
      email: AuthHelper.getLogin(),
      newPassword: this.password,
    };

    fetch('https://localhost:44381/api/userdataeditor/editpassword', {
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
          alert('You have successfully reset your password!');
          this.letterMessage = `You have successfully reset your password on Apartstep.fun with ${this.email}!`;
          this.sendInfoLetter();
          this.router.navigate(['']);
        } else {
          alert(response.message);
        }
        this.authService.setResetPasswordCondition(false);
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  MustMatch(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];

      if (matchingControl.errors && !matchingControl.errors.mustMatch) {
        // return if another validator has already found an error on the matchingControl
        return;
      }

      // set error on matchingControl if validation fails
      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ mustMatch: true });
      } else {
        matchingControl.setErrors(null);
      }
    };
  }

  ngOnInit(): void {
    if (
      !AuthHelper.isLogged() ||
      !this.authService.getResetPasswordCondition()
    ) {
      this.router.navigate(['']);
      return;
    }
  }
}
