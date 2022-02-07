import { Component, OnInit } from '@angular/core';
import { FacilityType } from 'src/app/models/facilitytype.item';

@Component({
  selector: 'app-facility-types-list',
  templateUrl: './facility-types-list.component.html',
  styleUrls: ['./facility-types-list.component.css']
})
export class FacilityTypesListComponent implements OnInit {

  types: FacilityType[] | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}
