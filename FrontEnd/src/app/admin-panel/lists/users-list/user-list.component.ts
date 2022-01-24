import { Component, OnInit } from '@angular/core';

import { User } from '../../../models/user.item';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  users: User[] | undefined;

  constructor() { }

  ngOnInit(): void {
  }

}
