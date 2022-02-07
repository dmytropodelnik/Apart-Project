import { Component, OnInit } from '@angular/core';
import { Facility } from 'src/app/models/facility.item';

@Component({
  selector: 'app-facilities-list',
  templateUrl: './facilities-list.component.html',
  styleUrls: ['./facilities-list.component.css']
})
export class FacilitiesListComponent implements OnInit {

  facilities: Facility[] | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}
