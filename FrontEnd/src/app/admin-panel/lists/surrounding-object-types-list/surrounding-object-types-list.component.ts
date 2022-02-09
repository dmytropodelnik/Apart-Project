import { Component, OnInit } from '@angular/core';
import { SurroundingObjectType } from 'src/app/models/Suggestions/surroundingobjecttype.item';

@Component({
  selector: 'app-surrounding-object-types-list',
  templateUrl: './surrounding-object-types-list.component.html',
  styleUrls: ['./surrounding-object-types-list.component.css']
})
export class SurroundingObjectTypesListComponent implements OnInit {

  types: SurroundingObjectType[] | null = null;

  constructor() { }

  ngOnInit(): void {
    fetch('https://localhost:44381/api/surroundingobjecttypes/gettypes', {
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
