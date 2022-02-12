import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {


  currentYear: number = new Date().getFullYear();
  content: string | undefined;

  constructor() {

  }

  setContent(newContent: string) {
    this.content = newContent;
  }

  ngOnInit(): void {

  }

}
