import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SurroundingObject } from 'src/app/models/Suggestions/surroundingobject.item';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-surrounding-objects-list',
  templateUrl: './surrounding-objects-list.component.html',
  styleUrls: ['./surrounding-objects-list.component.css'],
})
export class SurroundingObjectsListComponent implements OnInit {
  objects: SurroundingObject[] | null = null;
  object: string | null = null;
  checkedObject: number | null = null;

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

  addObject(): void {
    let object = {
      name: this.object,
    };

    fetch('https://apartmain.azurewebsites.net/api/surroundingobjects/addobject', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(object),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getObjects();
        } else {
          this.showAlert('Adding error!');
        }
        this.object = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  editObject(): void {
    let object = {
      id: this.checkedObject,
      name: this.object,
    };

    fetch('https://apartmain.azurewebsites.net/api/surroundingobjects/editobject', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(object),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getObjects();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.object = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  deleteObject(): void {
    let object = {
      id: this.checkedObject,
      name: this.object,
    };

    fetch('https://apartmain.azurewebsites.net/api/surroundingobjects/deleteobject', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(object),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getObjects();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.object = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  getObjects(): void {
<<<<<<< HEAD
    fetch(`https://apartmain.azurewebsites.net/api/surroundingobjects/getobjects?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
=======
    fetch(
      `https://localhost:44381/api/surroundingobjects/getobjects?page=${this.page}&pageSize=${this.pageSize}`,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
>>>>>>> backend
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.objects = data.objects;
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  collectElements(objects: SurroundingObject[]): void {
    for (let item of objects) {
      this.objects?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

<<<<<<< HEAD
    fetch(`https://apartmain.azurewebsites.net/api/surroundingobjects/getobjects?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
=======
    fetch(
      `https://localhost:44381/api/surroundingobjects/getobjects?page=${this.page}&pageSize=${this.pageSize}`,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
>>>>>>> backend
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.objects);
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  setObject(id: number | null, object: any): void {
    this.checkedObject = id;
    this.object = object;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getObjects();
  }
}
