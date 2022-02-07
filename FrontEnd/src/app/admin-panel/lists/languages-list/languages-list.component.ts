import { Component, OnInit } from '@angular/core';
import { Language } from 'src/app/models/language.item';

@Component({
  selector: 'app-languages-list',
  templateUrl: './languages-list.component.html',
  styleUrls: ['./languages-list.component.css']
})
export class LanguagesListComponent implements OnInit {

  languages: Language[] | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}
