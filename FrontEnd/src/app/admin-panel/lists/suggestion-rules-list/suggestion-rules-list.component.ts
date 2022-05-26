import { Component, OnInit } from '@angular/core';
import { SuggestionRule } from 'src/app/models/Suggestions/suggestionrule.item';
import { AdminContentService } from 'src/app/services/admin-content.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-suggestion-rules-list',
  templateUrl: './suggestion-rules-list.component.html',
  styleUrls: ['./suggestion-rules-list.component.css']
})
export class SuggestionRulesListComponent implements OnInit {

  rules: SuggestionRule[] | null = null;
  rule: SuggestionRule | null = null;
  searchRule: string = '';
  checkedRule: number | null = null;

  page: number = 1;
  pageSize: number = 15;

  constructor(
    private adminContentService: AdminContentService
  ) {

  }

  search(): void {
    fetch('https://apartmain.azurewebsites.net/api/suggestionrules/search?rule=' + this.searchRule, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.rules = data.rules;
        } else {
          alert('Search error!');
        }
        this.searchRule = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addRule(): void {
    let rule = {
      name: this.rule,
    };

    fetch('https://apartmain.azurewebsites.net/api/suggestionrules/addrule', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
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
        this.rule = null;
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

    fetch('https://apartmain.azurewebsites.net/api/suggestionrules/editrule', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(rule),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getRules();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.rule = null;
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

    fetch('https://apartmain.azurewebsites.net/api/suggestionrules/deleterule', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(rule),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getRules();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.rule = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getRules(): void {
    fetch(`https://apartmain.azurewebsites.net/api/suggestionrules/getrules?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
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


  collectElements(rules: SuggestionRule[]): void {
    for (let item of rules) {
      this.rules?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(`https://apartmain.azurewebsites.net/api/suggestionrules/getrules?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.rules);
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setRule(rule: SuggestionRule): void {
    this.checkedRule = rule.id;
    this.rule = rule;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getRules();
  }

}
