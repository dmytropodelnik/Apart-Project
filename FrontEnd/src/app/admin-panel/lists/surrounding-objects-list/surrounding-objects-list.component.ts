import { Component, OnInit } from '@angular/core';
import { SurroundingObject } from 'src/app/models/Suggestions/surroundingobject.item';

import AuthHelper from '../../../utils/authHelper';

@Component({
  selector: 'app-surrounding-objects-list',
  templateUrl: './surrounding-objects-list.component.html',
  styleUrls: ['./surrounding-objects-list.component.css']
})
export class SurroundingObjectsListComponent implements OnInit {

  objects: SurroundingObject[] | null = null;
  object: string | null = null;
  checkedObject: number | null = null;

  constructor() {}

  addObject(): void {
    let object = {
      name: this.object,
    };

    fetch('https://localhost:44381/api/objects/addobject', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(object),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getObjects();
        } else {
          alert('Adding error!');
        }
        this.object = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editObject(): void {
    let object = {
      id: this.checkedObject,
      name: this.object,
    };

    fetch('https://localhost:44381/api/objects/editobject', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(object),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getObjects();
        } else {
          alert('Editing error!');
        }
        this.object = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteObject(): void {
    let object = {
      id: this.checkedObject,
      name: this.object,
    };

    fetch('https://localhost:44381/api/objects/deleteobject', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(object),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getObjects();
        } else {
          alert('Editing error!');
        }
        this.object = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getObjects(): void {
    fetch('https://localhost:44381/api/objects/getobjects', {
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
