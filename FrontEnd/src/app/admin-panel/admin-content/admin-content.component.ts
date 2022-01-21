import { ThisReceiver } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-admin-content',
  templateUrl: './admin-content.component.html',
  styleUrls: ['./admin-content.component.css']
})
export class AdminContentComponent implements OnInit {

  content: string | undefined;

  constructor() {
    this.content = "users";
  }

  setContent(newContent: string) {
    this.content = newContent;
  }

  ngOnInit(): void {
  }

}
