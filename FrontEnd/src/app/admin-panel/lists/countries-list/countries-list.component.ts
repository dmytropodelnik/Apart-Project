import { Component, OnInit } from '@angular/core';
import { Country } from 'src/app/models/Location/country.item';

@Component({
  selector: 'app-countries-list',
  templateUrl: './countries-list.component.html',
  styleUrls: ['./countries-list.component.css'],
})
export class CountriesListComponent implements OnInit {

  countries: Country[] | null = null;

  constructor() {}

  ngOnInit(): void {
    fetch('https://localhost:44381/api/countries/getcountries', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.countries = data.countries;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }
}
