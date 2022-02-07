import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-notifications-list',
  templateUrl: './notifications-list.component.html',
  styleUrls: ['./notifications-list.component.css']
})
export class NotificationsListComponent implements OnInit {

  notifications: Notification[] | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}
