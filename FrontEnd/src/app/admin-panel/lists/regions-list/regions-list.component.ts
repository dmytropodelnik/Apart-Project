import { Component, OnInit } from '@angular/core';
import { Region } from 'src/app/models/Location/region.item';

@Component({
  selector: 'app-regions-list',
  templateUrl: './regions-list.component.html',
  styleUrls: ['./regions-list.component.css']
})
export class RegionsListComponent implements OnInit {

  regions: Region[] | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}
