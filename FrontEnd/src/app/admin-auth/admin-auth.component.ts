import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';

import { AuthorizationService } from '../services/authorization.service';

import AuthHelper from '../utils/authHelper';

import {
  FormBuilder,
  FormGroup,
  Validators,
  AbstractControl,
} from '@angular/forms';
import { RepositoryEnum } from '../enums/repositoryenum.item';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MainDataService } from '../services/main-data.service';

@Component({
  selector: 'app-admin-auth',
  templateUrl: './admin-auth.component.html',
  styleUrls: ['./admin-auth.component.css'],
})
export class AdminAuthComponent implements OnInit {
  login: string = '';
  password: string = '';
  loginForm: FormGroup;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private authService: AuthorizationService,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {
    this.loginForm = this.formBuilder.group({
      login: ['', [Validators.required, Validators.minLength(8)]],
      password: ['', [Validators.required, Validators.minLength(8)]],
    });
  }
  get f() {
    return this.loginForm.controls;
  }

  loginAdmin(): void {
    let user = {
      email: this.login,
      passwordHash: this.password,
      repository: RepositoryEnum.Enter,
    };

    fetch('https://localhost:44381/api/admin/login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(user),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          fetch('https://localhost:44381/token', {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json; charset=utf-8',
            },
            body: JSON.stringify(user),
          })
            .then((response) => response.json())
            .then((response) => {
              if (response.code !== 400) {
                this.authService.setTokenKey(response.encodedJwt);
                this.authService.toggleLogCondition();
                this.authService.setIsAdmin(true);

                AuthHelper.saveAuth(user.email, response.encodedJwt);

                alert('You have successfully authenticated as an admin!');

                this.router.navigate(['/admin']);
              } else {
                alert('Token fetching error!');
              }
            })
            .catch((ex) => {
              this.mainDataService.alertContent = ex;
              this.modalService.open(this.alert);
            });
        } else {
          alert('Incorrect data');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  ngOnInit(): void {}
}
