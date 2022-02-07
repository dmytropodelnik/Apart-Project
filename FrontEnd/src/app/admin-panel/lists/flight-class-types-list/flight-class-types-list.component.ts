import { Component, OnInit } from '@angular/core';
import { FlightClassType } from 'src/app/models/Flights/flightclasstype.item';

@Component({
  selector: 'app-flight-class-types-list',
  templateUrl: './flight-class-types-list.component.html',
  styleUrls: ['./flight-class-types-list.component.css']
})
export class FlightClassTypesListComponent implements OnInit {

  types: FlightClassType[] | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}
