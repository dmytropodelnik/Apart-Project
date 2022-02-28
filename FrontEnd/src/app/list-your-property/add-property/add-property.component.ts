import { Component, OnInit } from '@angular/core';

import { ListNewPropertyService } from '../../services/list-new-property.service';

import AuthHelper from '../../utils/authHelper';

@Component({
  selector: 'app-add-property',
  templateUrl: './add-property.component.html',
  styleUrls: ['./add-property.component.css'],
})
export class AddPropertyComponent implements OnInit {
  constructor(
    private listNewPropertyService: ListNewPropertyService,
  ) {

  }
  choice = 0;

  addApartment(): void {
    (this.choice = 1);
    console.log(this.choice);
  }
  addHome(): void {
    (this.choice = 2);
  }

  ngOnInit(): void {

  }
}
