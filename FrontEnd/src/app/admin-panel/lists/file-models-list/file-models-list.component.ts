import { Component, OnInit } from '@angular/core';
import { FileModel } from 'src/app/models/filemodel.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';
import ImageHelper from '../../../utils/imageHelper';
import { AdminContentService } from 'src/app/services/admin-content.service';

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
  uploadedFile: File | null = null;
  searchFile: string = '';

  page: number = 1;
  pageSize: number = 10;

  constructor(
    private adminContentService: AdminContentService
  ) {

  }

  search(): void {
    fetch('https://apartmain.azurewebsites.net/api/files/search?file=' + this.searchFile, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.files = data.files;
        } else {
          alert('Search error!');
        }
        this.searchFile = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addFile(): void {
    let file = {
      name: this.name,
      path: this.path,
    };

    fetch('https://apartmain.azurewebsites.net/api/files/addimage', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
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

    fetch('https://apartmain.azurewebsites.net/api/files/deleteimage', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
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
    fetch(`https://apartmain.azurewebsites.net/api/files/getimages?page=${this.page}&pageSize=${this.pageSize}`, {
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

  uploadFile() {
    let fData = new FormData();
    if (this.uploadedFile != null) {
      fData.append('uploadedFile', this.uploadedFile);
    }

      fetch('https://apartmain.azurewebsites.net/api/fileuploader/uploadfile', {
        method: 'POST',
        headers: {
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
        body: fData,
      })
      .then(r => r.json())
      .then(r => {
        if (r.code === 200) {
          alert("File has been successfully uploaded!");
          this.getFiles();
        } else {
          alert("Uploading error!");
        }
      })
      .catch(err => {
        alert(err);
      });
  }

  handleFileInput(files: FileList): void {
    if (files !== null) {
      this.uploadedFile = files.item(0);
    }
  }

  collectElements(files: FileModel[]): void {
    for (let item of files) {
      this.files?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(`https://apartmain.azurewebsites.net/api/files/getimages?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.files);
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
