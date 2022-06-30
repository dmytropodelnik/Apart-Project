import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { RoomType } from 'src/app/models/Suggestions/roomtype.item';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-room-types-list',
  templateUrl: './room-types-list.component.html',
  styleUrls: ['./room-types-list.component.css'],
})
export class RoomTypesListComponent implements OnInit {
  roomTypes: RoomType[] | null = null;
  type: RoomType | null = null;
  searchType: string = '';
  checkedType: number | null = null;

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
    fetch(
      'https://localhost:44381/api/roomtypes/search?type=' + this.searchType,
      {
        method: 'GET',
        headers: {
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.roomTypes = data.roomTypes;
        } else {
          this.showAlert('Search error!');
        }
        this.searchType = '';
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
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
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(type),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getTypes();
        } else {
          this.showAlert('Adding error!');
        }
        this.type = null;
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
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
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(type),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getTypes();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.type = null;
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
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
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(type),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getTypes();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Deleting error!');
        }
        this.type = null;
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  getTypes(): void {
    fetch(
      `https://localhost:44381/api/roomtypes/gettypes?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.roomTypes = data.types;
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  collectElements(types: RoomType[]): void {
    for (let item of types) {
      this.roomTypes?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(
      `https://localhost:44381/api/roomtypes/gettypes?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.collectElements(data.types);
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
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
