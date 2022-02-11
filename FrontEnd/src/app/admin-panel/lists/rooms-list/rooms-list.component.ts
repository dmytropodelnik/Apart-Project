import { Component, OnInit } from '@angular/core';
import { Room } from 'src/app/models/Suggestions/room.item';

import AuthHelper from '../../../utils/authHelper';

@Component({
  selector: 'app-rooms-list',
  templateUrl: './rooms-list.component.html',
  styleUrls: ['./rooms-list.component.css']
})
export class RoomsListComponent implements OnInit {

  rooms: Room[] | null = null;
  room: string | null = null;
  checkedRoom: number | null = null;

  isEditEnabled: boolean = true;
  isDeleteEnabled: boolean = true;

  constructor() { }

  addRoom(): void {
    let room = {
      title: this.room,
    };

    fetch('https://localhost:44381/api/rooms/addroom', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(room),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getRooms();
        } else {
          alert('Adding error!');
        }
        this.room = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editRoom(): void {
    let room = {
      id: this.checkedRoom,
      title: this.room,
    };

    fetch('https://localhost:44381/api/rooms/editroom', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(room),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getRooms();
        } else {
          alert('Editing error!');
        }
        this.room = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteRoom(): void {
    let room = {
      id: this.checkedRoom,
      title: this.room,
    };

    fetch('https://localhost:44381/api/rooms/deleteroom', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(room),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getRooms();
        } else {
          alert('Editing error!');
        }
        this.room = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getRooms(): void {
    fetch('https://localhost:44381/api/rooms/getrooms', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.rooms = data.rooms;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setRoom(id: number | null, room: string): void {
    this.checkedRoom = id;
    this.room = room;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getRooms();
  }

}
