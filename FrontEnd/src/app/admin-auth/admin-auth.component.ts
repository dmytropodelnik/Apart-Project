import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthorizationService } from '../services/authorization.service';

import {
  FormBuilder,
  FormGroup,
  Validators,
  AbstractControl,
} from '@angular/forms';

@Component({
  selector: 'app-admin-auth',
  templateUrl: './admin-auth.component.html',
  styleUrls: ['./admin-auth.component.css'],
})
export class AdminAuthComponent implements OnInit {
  login: string | undefined;
  password: string | undefined;
  loginForm: FormGroup;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private authService: AuthorizationService) {
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
    };

    fetch('https://apartproject.azurewebsites.net/api/admin/login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(user),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.authService.setIsAdmin(true);
          this.router.navigate(['/admin']);
        } else {
          alert('Incorrect data');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  ngOnInit(): void {}
}
