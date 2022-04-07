import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-security-list',
  templateUrl: './security-list.component.html',
  styleUrls: ['./security-list.component.css']
})
export class SecurityListComponent implements OnInit {
  isEditing: boolean[] = [];

  constructor() { }

  setCondition(id: number): void {
    this.isEditing[id] = !this.isEditing[id];
  }

  ngOnInit(): void {
    for (let i = 0; i < 13; i++) {
      this.isEditing[i] = false;
    }
  }

}
