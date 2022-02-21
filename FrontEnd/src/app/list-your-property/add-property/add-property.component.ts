import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-add-property',
  templateUrl: './add-property.component.html',
  styleUrls: ['./add-property.component.css'],
})
export class AddPropertyComponent implements OnInit {
  constructor() {}
  choice = 0;

  addApartment(): void{
    (this.choice = 1);
    console.log(this.choice);
  }
  addHome(): void{
    (this.choice = 2);
  }

  ngOnInit(): void {}
}
