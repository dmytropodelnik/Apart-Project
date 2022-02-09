import { Component, OnInit } from '@angular/core';
import { Airport } from 'src/app/models/Location/airport.item';

@Component({
  selector: 'app-airports-list',
  templateUrl: './airports-list.component.html',
  styleUrls: ['./airports-list.component.css']
})
export class AirportsListComponent implements OnInit {

  airports: Airport[] | null = null;

  constructor() { }

  ngOnInit(): void {
    fetch('https://localhost:44381/api/airports/getairports', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.airports = data.airports;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

}
