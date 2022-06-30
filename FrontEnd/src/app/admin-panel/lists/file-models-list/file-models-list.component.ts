import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FileModel } from 'src/app/models/filemodel.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';
import ImageHelper from '../../../utils/imageHelper';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-file-models-list',
  templateUrl: './file-models-list.component.html',
  styleUrls: ['./file-models-list.component.css'],
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
    fetch('https://localhost:44381/api/files/search?file=' + this.searchFile, {
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
          this.showAlert('Search error!');
        }
        this.searchFile = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

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
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(file),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getFiles();
        } else {
          this.showAlert('Adding error!');
        }
        this.name = '';
        this.path = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
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
          this.showAlert('Deleting error!');
        }
        this.name = '';
        this.path = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  getFiles(): void {
    fetch(
      `https://localhost:44381/api/files/getimages?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.files = data.files;
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  uploadFile() {
    let fData = new FormData();
    if (this.uploadedFile != null) {
      fData.append('uploadedFile', this.uploadedFile);
    }

    fetch('https://localhost:44381/api/fileuploader/uploadfile', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: fData,
    })
      .then((r) => r.json())
      .then((r) => {
        if (r.code === 200) {
          this.showAlert('File has been successfully uploaded!');
          this.getFiles();
        } else {
          this.showAlert('Uploading error!');
        }
      })
      .catch((err) => {
        this.mainDataService.alertContent = err;
        this.modalService.open(this.alert);
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

    fetch(
      `https://localhost:44381/api/files/getimages?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.collectElements(data.files);
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
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
