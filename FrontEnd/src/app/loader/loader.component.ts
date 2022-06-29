import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { MainDataService } from '../services/main-data.service';

@Component({
  selector: 'app-loader',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.css'],
})
export class LoaderComponent implements OnInit {
  constructor() {}

  ngOnInit(): void {}
}
