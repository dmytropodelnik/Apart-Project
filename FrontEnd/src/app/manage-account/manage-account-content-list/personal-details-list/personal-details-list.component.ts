import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/UserData/user.item';

@Component({
  selector: 'app-personal-details-list',
  templateUrl: './personal-details-list.component.html',
  styleUrls: ['./personal-details-list.component.css']
})
export class PersonalDetailsListComponent implements OnInit {
  isEditing: boolean[] = [];

  user: User = new User();

  constructor() { }

  setCondition(id: number): void {
    this.isEditing[id] = !this.isEditing[id];
  }

  ngOnInit(): void {

  }

}
