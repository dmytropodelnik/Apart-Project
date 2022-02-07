import { Component, OnInit } from '@angular/core';
import { City } from 'src/app/models/Location/city.item';

@Component({
  selector: 'app-cities-list',
  templateUrl: './cities-list.component.html',
  styleUrls: ['./cities-list.component.css']
})
export class CitiesListComponent implements OnInit {

  cities: City[] | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}
