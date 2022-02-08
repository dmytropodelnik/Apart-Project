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
    fetch('https://localhost:44381/api/countries/getcountries', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.rules = data.rules;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

}
