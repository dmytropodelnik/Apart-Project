import { Component, OnInit } from '@angular/core';
import { SuggestionRule } from 'src/app/models/Suggestions/suggestionrule.item';

@Component({
  selector: 'app-suggestion-rules-list',
  templateUrl: './suggestion-rules-list.component.html',
  styleUrls: ['./suggestion-rules-list.component.css']
})
export class SuggestionRulesListComponent implements OnInit {

  rules: SuggestionRule[] | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}
