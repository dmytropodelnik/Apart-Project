import { Component, OnInit } from '@angular/core';
import { RoomType } from 'src/app/models/Suggestions/roomtype.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-room-types-list',
  templateUrl: './room-types-list.component.html',
  styleUrls: ['./room-types-list.component.css']
})
export class RoomTypesListComponent implements OnInit {

  roomTypes: RoomType[] | null = null;
  type: RoomType | null = null;
  searchType: string = '';
  checkedType: number | null = null;

  isEditEnabled: boolean = true;
  isDeleteEnabled: boolean = true;

  constructor() { }

  search(): void {
    fetch('https://localhost:44381/api/roomtypes/search?type=' + this.searchType, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.roomTypes = data.roomTypes;
        } else {
          alert('Search error!');
        }
        this.searchType = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addType(): void {
    let type = {
      title: this.type,
    };

    fetch('https://localhost:44381/api/roomtypes/addtype', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(type),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getTypes();
        } else {
          alert('Adding error!');
        }
        this.type = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editType(): void {
    let type = {
      id: this.checkedType,
      title: this.type,
    };

    fetch('https://localhost:44381/api/roomtypes/edittype', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(type),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getTypes();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.type = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteType(): void {
    let type = {
      id: this.checkedType,
      title: this.type,
    };

    fetch('https://localhost:44381/api/roomtypes/deletetype', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(type),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getTypes();
          ListHelper.disableButtons();
        } else {
          alert('Deleting error!');
        }
        this.type = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getTypes(): void {
    fetch('https://localhost:44381/api/roomtypes/gettypes', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.roomTypes = data.types;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setType(type: RoomType): void {
    this.checkedType = type.id;
    this.type = type;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getTypes();
  }

}
