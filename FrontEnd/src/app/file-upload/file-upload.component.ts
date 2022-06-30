import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MainDataService } from '../services/main-data.service';
import AuthHelper from '../utils/authHelper';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css'],
})
export class FileUploadComponent implements OnInit {
  uploadedFile: File | null = null;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  uploadFile() {
    let fData = new FormData();
    if (this.uploadedFile != null) {
      fData.append('uploadedFile', this.uploadedFile);
    }

    fetch('https://apartmain.azurewebsites.net/api/fileuploader/uploadfile', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: fData,
    })
      .then((r) => r.json())
      .then((r) => {
        if (r.code === 200) {
          this.showAlert('File has been successfully uploaded!');
        } else {
          this.showAlert('Uploading error!');
        }
      })
      .catch((err) => {
        this.mainDataService.alertContent = err;
        this.modalService.open(this.alert);
      });
  }

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  handleFileInput(files: FileList): void {
    if (files !== null) {
      this.uploadedFile = files.item(0);
    }
  }

  ngOnInit(): void {}
}
