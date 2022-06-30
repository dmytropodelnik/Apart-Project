import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Suggestion } from 'src/app/models/Suggestions/suggestion.item';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-suggestions-list',
  templateUrl: './suggestions-list.component.html',
  styleUrls: ['./suggestions-list.component.css'],
})
export class SuggestionsListComponent implements OnInit {
  suggestions: Suggestion[] | null = null;
  suggestion: Suggestion | null = null;
  checkedSuggestion: number | null = null;

  page: number = 1;
  pageSize: number = 5;

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

  addSuggestion(): void {
    let suggestion = {
      name: this.suggestion,
    };

    fetch('https://localhost:44381/api/suggestions/addsuggestion', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(suggestion),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getSuggestions();
        } else {
          this.showAlert('Adding error!');
        }
        this.suggestion = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  editSuggestion(): void {
    let suggestion = {
      id: this.checkedSuggestion,
      name: this.suggestion,
    };

    fetch('https://localhost:44381/api/suggestions/editsuggestion', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(suggestion),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getSuggestions();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.suggestion = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  deleteSuggestion(): void {
    let suggestion = {
      id: this.checkedSuggestion,
      name: this.suggestion,
    };

    fetch('https://localhost:44381/api/suggestions/deletesuggestion', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(suggestion),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getSuggestions();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.suggestion = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  getSuggestions(): void {
    fetch(
      `https://localhost:44381/api/suggestions/getsuggestions?page=${this.page}&pageSize=${this.pageSize}`,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.suggestions = data.suggestions;
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  collectElements(suggestions: Suggestion[]): void {
    for (let item of suggestions) {
      this.suggestions?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(
      `https://localhost:44381/api/suggestions/getsuggestions?page=${this.page}&pageSize=${this.pageSize}`,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.suggestions);
          //this.suggestions = data.suggestions;
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  setSuggestion(suggestion: Suggestion): void {
    this.checkedSuggestion = suggestion.id;
    this.suggestion = suggestion;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getSuggestions();
  }
}
