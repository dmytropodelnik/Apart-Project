import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SuggestionRule } from 'src/app/models/Suggestions/suggestionrule.item';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-suggestion-rules-list',
  templateUrl: './suggestion-rules-list.component.html',
  styleUrls: ['./suggestion-rules-list.component.css'],
})
export class SuggestionRulesListComponent implements OnInit {
  rules: SuggestionRule[] | null = null;
  rule: SuggestionRule | null = null;
  searchRule: string = '';
  checkedRule: number | null = null;

  page: number = 1;
  pageSize: number = 15;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private adminContentService: AdminContentService,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  search(): void {
<<<<<<< HEAD
    fetch('https://apartmain.azurewebsites.net/api/suggestionrules/search?rule=' + this.searchRule, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
=======
    fetch(
      'https://localhost:44381/api/suggestionrules/search?rule=' +
        this.searchRule,
      {
        method: 'GET',
        headers: {
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
>>>>>>> backend
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.rules = data.rules;
        } else {
          this.showAlert('Search error!');
        }
        this.searchRule = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
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
          this.showAlert('Adding error!');
        }
        this.rule = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
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
          this.showAlert('Editing error!');
        }
        this.rule = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
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
          this.showAlert('Editing error!');
        }
        this.rule = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  getRules(): void {
<<<<<<< HEAD
    fetch(`https://apartmain.azurewebsites.net/api/suggestionrules/getrules?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
=======
    fetch(
      `https://localhost:44381/api/suggestionrules/getrules?page=${this.page}&pageSize=${this.pageSize}`,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
>>>>>>> backend
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.rules = data.rules;
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  collectElements(rules: SuggestionRule[]): void {
    for (let item of rules) {
      this.rules?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

<<<<<<< HEAD
    fetch(`https://apartmain.azurewebsites.net/api/suggestionrules/getrules?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
=======
    fetch(
      `https://localhost:44381/api/suggestionrules/getrules?page=${this.page}&pageSize=${this.pageSize}`,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
>>>>>>> backend
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.rules);
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
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
