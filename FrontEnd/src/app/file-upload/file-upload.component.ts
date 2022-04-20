import { Component, OnInit } from '@angular/core';
import AuthHelper from '../utils/authHelper';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent implements OnInit {
  uploadedFile: File | null = null;

  constructor() { }

  uploadFile() {
    let fData = new FormData();
    if (this.uploadedFile != null) {
      fData.append('uploadedFile', this.uploadedFile);
    }

      fetch('http://apartmain.azurewebsites.net/api/fileuploader/uploadfile', {
        method: 'POST',
        headers: {
          "Accept": "application/json",
          "Authorization": "Bearer " + AuthHelper.getToken(),
         },
        body: fData,
      })
      .then(r => r.json())
      .then(r => {
        if (r.code === 200) {
          alert("File has been successfully uploaded!");
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

  ngOnInit(): void {
  }

}
