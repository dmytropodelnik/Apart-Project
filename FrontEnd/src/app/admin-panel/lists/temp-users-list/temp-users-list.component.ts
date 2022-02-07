import { Component, OnInit } from '@angular/core';
import { TempUser } from 'src/app/models/UserData/tempuser.item';

@Component({
  selector: 'app-temp-users-list',
  templateUrl: './temp-users-list.component.html',
  styleUrls: ['./temp-users-list.component.css']
})
export class TempUsersListComponent implements OnInit {

  users: TempUser[] | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}
