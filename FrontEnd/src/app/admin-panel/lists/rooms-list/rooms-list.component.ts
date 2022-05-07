import { Component, OnInit } from '@angular/core';
import { Room } from 'src/app/models/Suggestions/room.item';
import { AdminContentService } from 'src/app/services/admin-content.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-rooms-list',
  templateUrl: './rooms-list.component.html',
  styleUrls: ['./rooms-list.component.css']
})
export class RoomsListComponent implements OnInit {

  rooms: Room[] | null = null;
  room: Room | null = null;
  searchRoom: string = '';
  checkedRoom: number | null = null;

  isEditEnabled: boolean = true;
  isDeleteEnabled: boolean = true;

  page: number = 1;
  pageSize: number = 15;

  constructor(
    private adminContentService: AdminContentService
  ) {

  }

  search(): void {
    fetch('https://apartmain.azurewebsites.net/api/rooms/search?room=' + this.searchRoom, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.rooms = data.rooms;
        } else {
          alert('Search error!');
        }
        this.searchRoom = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addRoom(): void {
    let room = {
      title: this.room,
    };

    fetch('https://apartmain.azurewebsites.net/api/rooms/addroom', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
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
        this.room = null;
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

    fetch('https://apartmain.azurewebsites.net/api/rooms/editroom', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(room),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getRooms();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.room = null;
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

    fetch('https://apartmain.azurewebsites.net/api/rooms/deleteroom', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(room),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getRooms();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.room = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getRooms(): void {
    fetch(`https://apartmain.azurewebsites.net/api/rooms/getrooms?page=${this.page}&pageSize=${this.pageSize}`, {
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

  collectElements(rooms: Room[]): void {
    for (let item of rooms) {
      this.rooms?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(`https://apartmain.azurewebsites.net/api/rooms/getrooms?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.rooms);
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setRoom(room: Room): void {
    this.checkedRoom = room.id;
    this.room = room;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getRooms();
  }

}
