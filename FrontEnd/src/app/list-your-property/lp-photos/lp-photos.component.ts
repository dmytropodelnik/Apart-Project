import { Component, OnInit } from '@angular/core';

import AuthHelper from '../../utils/authHelper';

@Component({
  selector: 'app-lp-photos',
  templateUrl: './lp-photos.component.html',
  styleUrls: ['./lp-photos.component.css']
})
export class LpPhotosComponent implements OnInit {
  savedPropertyId: string = '';
  uploadedFiles: File[] | null = null;


  constructor() { }

  addPropertyPhotos(): void {
    let counter = 1;
    let fData = new FormData();
    if (this.uploadedFiles != null) {
      for (let file of this.uploadedFiles) {
        fData.append('uploadedFile' + counter, file);
        counter++;
      }
    }

    fetch('https://localhost:44381/api/listnewproperty/addphotos?suggestionId=' + this.savedPropertyId, {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: fData,
    })
      .then((r) => r.json())
      .then((r) => {
        if (r.code === 200) {
          alert('Files have been successfully uploaded!');
        } else {
          alert('Uploading error!');
        }
      })
      .catch((err) => {
        alert(err);
      });
  }

  ngOnInit(): void {
    this.addPropertyPhotos();
  }
}
