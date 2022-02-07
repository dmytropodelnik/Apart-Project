import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { User } from '../models/UserData/user.item';
import AuthHelper from '../utils/authHelper';

import { AuthorizationService } from '../services/authorization.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  public isCollapsed = false;
  user: User | null = null;

  @ViewChild('content') content!: TemplateRef<any>;
  @ViewChild('content1') content1!: TemplateRef<any>;

  constructor(
    config: NgbModalConfig,
    private modalService: NgbModal,
    public authService: AuthorizationService,
  ) {

  }

  getToken(): string {
    return AuthHelper.getToken();
  }

  ngOnInit(): void {

  }

  open() {
    this.modalService.open(this.content, { size: 'lg', centered: true });
  }

  open1() {
    this.modalService.open(this.content1, { size: 'lg', centered: true });
  }
}
