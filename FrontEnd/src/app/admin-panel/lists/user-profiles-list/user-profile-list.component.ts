import { Component, OnInit } from '@angular/core';
import { UserProfile } from 'src/app/models/UserData/useprofile.item';

@Component({
  selector: 'app-user-profile-list',
  templateUrl: './user-profile-list.component.html',
  styleUrls: ['./user-profile-list.component.css']
})
export class UserProfileListComponent implements OnInit {

  profiles: UserProfile[] | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}
