import { Component, OnInit } from '@angular/core';
import { FileModel } from 'src/app/models/filemodel.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';
import ImageHelper from '../../../utils/imageHelper';

@Component({
  selector: 'app-file-models-list',
  templateUrl: './file-models-list.component.html',
  styleUrls: ['./file-models-list.component.css']
})
export class FileModelsListComponent implements OnInit {

  files: FileModel[] | null = null;
  name: string | null = null;
  path: string | null = null;
  checkedFile: number | null = null;
  imageHelper: any = ImageHelper;

  constructor() {}

  addFile(): void {
    let file = {
      name: this.name,
      path: this.path,
    };

    fetch('https://localhost:44381/api/files/addimage', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(file),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getFiles();
        } else {
          alert('Adding error!');
        }
        this.name = '';
        this.path = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteFile(): void {
    let file = {
      id: this.checkedFile,
      name: this.name,
      path: this.path,
    };

    fetch('https://localhost:44381/api/files/deleteimage', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(file),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getFiles();
          ListHelper.disableButtons();
        } else {
          alert('Deleting error!');
        }
        this.name = '';
        this.path = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getFiles(): void {
    fetch('https://localhost:44381/api/files/getimages', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.files = data.files;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setFile(id: number | null, name: string, path: string): void {
    this.checkedFile = id;
    this.name = name;
    this.path = path;

    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getFiles();
  }

}
