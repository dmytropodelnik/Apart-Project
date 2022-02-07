import { Component, OnInit } from '@angular/core';

import { User } from '../../../models/UserData/user.item';

@Component({
  selector: 'app-users-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  users: User[] | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}
