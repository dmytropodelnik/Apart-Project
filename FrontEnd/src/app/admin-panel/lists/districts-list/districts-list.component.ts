import { Component, OnInit } from '@angular/core';
import { District } from 'src/app/models/Location/district.item';

@Component({
  selector: 'app-districts-list',
  templateUrl: './districts-list.component.html',
  styleUrls: ['./districts-list.component.css']
})
export class DistrictsListComponent implements OnInit {

  districts: District[] | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}
