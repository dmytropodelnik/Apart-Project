import { Component, OnInit } from '@angular/core';
import { SuggestionRule } from 'src/app/models/Suggestions/suggestionrule.item';

import AuthHelper from '../../../utils/authHelper';

@Component({
  selector: 'app-suggestion-rules-list',
  templateUrl: './suggestion-rules-list.component.html',
  styleUrls: ['./suggestion-rules-list.component.css']
})
export class SuggestionRulesListComponent implements OnInit {

  rules: SuggestionRule[] | null = null;
  rule: string | null = null;
  checkedRule: number | null = null;

  constructor() {}

  addRule(): void {
    let rule = {
      name: this.rule,
    };

    fetch('https://localhost:44381/api/rules/addrule', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(rule),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getRules();
        } else {
          alert('Adding error!');
        }
        this.rule = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editRule(): void {
    let rule = {
      id: this.checkedRule,
      name: this.rule,
    };

    fetch('https://localhost:44381/api/rules/editrule', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(rule),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getRules();
        } else {
          alert('Editing error!');
        }
        this.rule = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteRule(): void {
    let rule = {
      id: this.checkedRule,
      name: this.rule,
    };

    fetch('https://localhost:44381/api/rules/deleterule', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(rule),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getRules();
        } else {
          alert('Editing error!');
        }
        this.rule = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getRules(): void {
    fetch('https://localhost:44381/api/rules/getrules', {
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

  setRule(id: number | null, rule: string): void {
    this.checkedRule = id;
    this.rule = rule;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getRules();
  }

}
