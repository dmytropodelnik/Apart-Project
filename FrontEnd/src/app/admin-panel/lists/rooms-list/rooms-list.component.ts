import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Room } from 'src/app/models/Suggestions/room.item';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-rooms-list',
  templateUrl: './rooms-list.component.html',
  styleUrls: ['./rooms-list.component.css'],
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

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private adminContentService: AdminContentService,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  search(): void {
    fetch('https://localhost:44381/api/rooms/search?room=' + this.searchRoom, {
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
          this.showAlert('Search error!');
        }
        this.searchRoom = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  addRoom(): void {
    let room = {
      title: this.room,
    };

    fetch('https://localhost:44381/api/rooms/addroom', {
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
          this.showAlert('Adding error!');
        }
        this.room = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
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
          this.showAlert('Editing error!');
        }
        this.room = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
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
          this.showAlert('Editing error!');
        }
        this.room = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  getRooms(): void {
    fetch(
      `https://localhost:44381/api/rooms/getrooms?page=${this.page}&pageSize=${this.pageSize}`,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.rooms = data.rooms;
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  collectElements(rooms: Room[]): void {
    for (let item of rooms) {
      this.rooms?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(
      `https://localhost:44381/api/rooms/getrooms?page=${this.page}&pageSize=${this.pageSize}`,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.rooms);
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
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
