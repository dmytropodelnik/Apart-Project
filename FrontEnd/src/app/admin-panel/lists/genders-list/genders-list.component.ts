import { Component, OnInit } from '@angular/core';
import { Gender } from 'src/app/models/UserData/gender.item';

@Component({
  selector: 'app-genders-list',
  templateUrl: './genders-list.component.html',
  styleUrls: ['./genders-list.component.css']
})
export class GendersListComponent implements OnInit {

  genders: Gender[] | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}
