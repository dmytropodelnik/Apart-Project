import { Component, OnInit } from '@angular/core';
import { RoomType } from 'src/app/models/Suggestions/roomtype.item';

@Component({
  selector: 'app-room-types-list',
  templateUrl: './room-types-list.component.html',
  styleUrls: ['./room-types-list.component.css']
})
export class RoomTypesListComponent implements OnInit {

  roomTypes: RoomType[] | null = null;

  constructor() { }

  ngOnInit(): void {
    fetch('https://localhost:44381/api/countries/getcountries', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.roomTypes = data.roomTypes;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

}
