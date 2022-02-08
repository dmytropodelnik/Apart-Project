import { Component, OnInit } from '@angular/core';
import { FileModel } from 'src/app/models/filemodel.item';

@Component({
  selector: 'app-file-models-list',
  templateUrl: './file-models-list.component.html',
  styleUrls: ['./file-models-list.component.css']
})
export class FileModelsListComponent implements OnInit {

  files: FileModel[] | null = null;

  constructor() { }

  ngOnInit(): void {
    fetch('https://localhost:44381/api/countries/getcountries', {
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

}
