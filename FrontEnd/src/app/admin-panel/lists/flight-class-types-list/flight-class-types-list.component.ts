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
    fetch('https://localhost:44381/api/countries/getcountries', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.types = data.type;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

}
