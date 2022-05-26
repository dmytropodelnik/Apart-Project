import { Component, OnInit,} from '@angular/core';
import { Router } from '@angular/router';

import { AuthorizationService } from '../../services/authorization.service';

import AuthHelper from '../../utils/authHelper';

@Component({
  selector: 'app-manage-account',
  templateUrl: './manage-account.component.html',
  styleUrls: ['./manage-account.component.css'],
})
export class ManageAccountComponent implements OnInit {
  currentYear: number = new Date().getFullYear();
  content: string = 'personal-details';

  constructor(
    private router: Router,
    private authService: AuthorizationService
  ) {}

  setContent(newContent: string) {
    this.content = newContent;
  }

  ngOnInit(): void {
    
  }
}
