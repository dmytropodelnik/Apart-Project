import { Component, OnInit } from '@angular/core';
import { SuggestionRuleType } from 'src/app/models/Suggestions/suggestionruletype.item';

@Component({
  selector: 'app-suggestion-rule-types-list',
  templateUrl: './suggestion-rule-types-list.component.html',
  styleUrls: ['./suggestion-rule-types-list.component.css']
})
export class SuggestionRuleTypesListComponent implements OnInit {

  ruleTypes: SuggestionRuleType[] | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}
