import { Component, OnInit } from '@angular/core';
import { SurroundingObject } from 'src/app/models/Suggestions/surroundingobject.item';

@Component({
  selector: 'app-surrounding-objects-list',
  templateUrl: './surrounding-objects-list.component.html',
  styleUrls: ['./surrounding-objects-list.component.css']
})
export class SurroundingObjectsListComponent implements OnInit {

  objects: SurroundingObject[] | null = null;

  constructor() { }

  ngOnInit(): void {
    fetch('https://localhost:44381/api/countries/getcountries', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.objects = data.objects;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

}
