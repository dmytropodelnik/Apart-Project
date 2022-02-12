import { Component, OnInit } from '@angular/core';
import { FileModel } from 'src/app/models/filemodel.item';

import AuthHelper from '../../../utils/authHelper';

@Component({
  selector: 'app-file-models-list',
  templateUrl: './file-models-list.component.html',
  styleUrls: ['./file-models-list.component.css']
})
export class FileModelsListComponent implements OnInit {

  files: FileModel[] | null = null;
  file: string | null = null;
  checkedFile: number | null = null;

  constructor() {}

  addFile(): void {
    let file = {
      name: this.file,
    };

    fetch('https://localhost:44381/api/files/addfile', {
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
        this.file = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editFile(): void {
    let file = {
      id: this.checkedFile,
      name: this.file,
    };

    fetch('https://localhost:44381/api/files/editfile', {
      method: 'PUT',
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
          alert('Editing error!');
        }
        this.file = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteFile(): void {
    let file = {
      id: this.checkedFile,
      name: this.file,
    };

    fetch('https://localhost:44381/api/files/deletefile', {
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
        } else {
          alert('Editing error!');
        }
        this.file = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getFiles(): void {
    fetch('https://localhost:44381/api/files/getfiles', {
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

  setFile(id: number | null, file: string): void {
    this.checkedFile = id;
    this.file = file;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getFiles();
  }

}
