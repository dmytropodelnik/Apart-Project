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
    fetch('https://localhost:44381/api/countries/getcountries', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.ruleTypes = data.ruleTypes;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

}
