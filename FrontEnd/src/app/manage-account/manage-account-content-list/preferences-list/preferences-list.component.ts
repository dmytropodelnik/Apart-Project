import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-preferences-list',
  templateUrl: './preferences-list.component.html',
  styleUrls: ['./preferences-list.component.css']
})
export class PreferencesListComponent implements OnInit {
  isEditing: boolean[] = [];

  constructor() { }

  setCondition(id: number): void {
    this.isEditing[id] = !this.isEditing[id];
  }

  ngOnInit(): void {
  }

}
