import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

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
    private activatedRouter: ActivatedRoute
  ) {}

  fileToUpload: any;
  imageUrl: any;
  selectedFiles?: FileList;
  previews: string[] = [];

  selectFiles(event: any): void {
    this.selectedFiles = event.target.files;

    //this.previews = [];

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
        this.uploadedFiles.push(files.item(i) as File);
      }
    }
  }

  addPropertyPhotos(): void {
    if (this.uploadedFiles == null) {
      alert('Upload files please!');
      return;
    }
    if (this.uploadedFiles.length < 8) {
      alert('You have to upload at least 8 images!');
      return;
    }

    let fData = new FormData();

    for (let i = 0; i < this.uploadedFiles.length; i++) {
      fData.append('uploadedFiles', this.uploadedFiles[i]);
    }
    console.log(fData.getAll('uploadedFiles'));
    fetch(
      'https://localhost:44381/api/listnewproperty/addphotos?suggestionId=' +
        this.listNewPropertyService.getSavedPropertyId(),
      {
        method: 'POST',
        headers: {
          // 'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
        body: fData,
      }
    )
      .then((r) => r.json())
      .then((r) => {
        if (r.code === 200) {
          console.log('Files have been successfully uploaded!');
          this.router.navigate(['/lp/reviewandcomplete']);
        } else {
          alert('Uploading error!');
        }
      })
      .catch((err) => {
        alert(err);
      });
  }

  ngOnInit(): void {
    this.activatedRouter.queryParams.subscribe((params: any) => {
      if (params['toSaveId'] == true) {
        this.listNewPropertyService.setSavedPropertyId(
          params['id']
        );
      }
    });
    if (!AuthHelper.isLogged()) {
      this.router.navigate(['']);
    } else if (!this.listNewPropertyService.getSavedPropertyId()) {
      this.router.navigate(['']);
    }
  }
}
