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
  }

}
