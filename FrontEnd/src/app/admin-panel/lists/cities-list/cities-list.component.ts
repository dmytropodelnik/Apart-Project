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
    fetch('https://localhost:44381/api/cities/getcities', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.cities = data.cities;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

}
