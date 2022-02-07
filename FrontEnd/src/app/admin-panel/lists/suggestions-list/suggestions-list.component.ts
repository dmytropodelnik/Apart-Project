import { Component, OnInit } from '@angular/core';
import { Suggestion } from 'src/app/models/Suggestions/suggestion.item';

@Component({
  selector: 'app-suggestions-list',
  templateUrl: './suggestions-list.component.html',
  styleUrls: ['./suggestions-list.component.css']
})
export class SuggestionsListComponent implements OnInit {

  suggestions: Suggestion[] | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}
