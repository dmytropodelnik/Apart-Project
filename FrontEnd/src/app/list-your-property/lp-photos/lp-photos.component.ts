import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-lp-photos',
  templateUrl: './lp-photos.component.html',
  styleUrls: ['./lp-photos.component.css'],
})
export class LpPhotosComponent implements OnInit {
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
  constructor() {}

  ngOnInit(): void {}
}
