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
    fetch('https://localhost:44381/api/countries/getcountries', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.types = data.types;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

}
