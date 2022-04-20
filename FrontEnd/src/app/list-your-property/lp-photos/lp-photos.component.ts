import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { ListNewPropertyService } from '../../services/list-new-property.service';

import AuthHelper from '../../utils/authHelper';

@Component({
  selector: 'app-lp-photos',
  templateUrl: './lp-photos.component.html',
  styleUrls: ['./lp-photos.component.css'],
})
export class LpPhotosComponent implements OnInit {
  savedPropertyId: string = '';
  uploadedFiles: File[] = [];

  constructor(
    private listNewPropertyService: ListNewPropertyService,
    private router: Router,
  ) {

  }

  fileToUpload: any;
  imageUrl: any;
  selectedFiles?: FileList;
  previews: string[] = [];

  selectFiles(event: any): void {
    this.selectedFiles = event.target.files;

    this.previews = [];

    if (this.selectedFiles && this.selectedFiles[0]) {
      const numberOfFiles = this.selectedFiles.length;
      for (let i = 0; i < numberOfFiles; i++) {
        const reader = new FileReader();

        reader.onload = (e: any) => {
          this.previews.push(e.target.result);
        };

        reader.readAsDataURL(this.selectedFiles[i]);
      }
    }
  }

  handleFileInput(files: FileList | null): void {
    if (files !== null) {
      for (let i = 0; i < files.length; i++) {
        this.uploadedFiles[i] = files.item(i) as File;
      }
    }
  }

  addPropertyPhotos(): void {
    if (this.uploadedFiles != null) {
      for (let i = 0; i < this.uploadedFiles.length; i++) {
        let fData = new FormData();
        fData.append('uploadedFile', this.uploadedFiles[i]);

        fetch('https://apartmain.azurewebsites.net/api/listnewproperty/addphotos?suggestionId=' + this.listNewPropertyService.getSavedPropertyId(), {
          method: 'POST',
          headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + AuthHelper.getToken(),
           },
          body: fData,
        })
          .then((r) => r.json())
          .then((r) => {
            if (r.code === 200) {
              console.log('Files have been successfully uploaded!' + ' ' + i);
              if (i == this.uploadedFiles.length - 1) {
                this.router.navigate(['/lp/pricingandcalender']);
              }
            } else {
              alert('Uploading error!');
            }
          })
          .catch((err) => {
            alert(err);
          });
      }
    }
  }

  ngOnInit(): void {

  }
}
