import { Component, OnInit } from '@angular/core';
import { Suggestion } from 'src/app/models/Suggestions/suggestion.item';
import { AdminContentService } from 'src/app/services/admin-content.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-suggestions-list',
  templateUrl: './suggestions-list.component.html',
  styleUrls: ['./suggestions-list.component.css']
})
export class SuggestionsListComponent implements OnInit {

  suggestions: Suggestion[] | null = null;
  suggestion: Suggestion | null = null;
  checkedSuggestion: number | null = null;

  page: number = 1;
  pageSize: number = 5;

  constructor(
    private adminContentService: AdminContentService
  ) {

  }

  addSuggestion(): void {
    let suggestion = {
      name: this.suggestion,
    };

    fetch('https://apartmain.azurewebsites.net/api/suggestions/addsuggestion', {
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
          alert('Adding error!');
        }
        this.suggestion = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editSuggestion(): void {
    let suggestion = {
      id: this.checkedSuggestion,
      name: this.suggestion,
    };

    fetch('https://apartmain.azurewebsites.net/api/suggestions/editsuggestion', {
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
          alert('Editing error!');
        }
        this.suggestion = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteSuggestion(): void {
    let suggestion = {
      id: this.checkedSuggestion,
      name: this.suggestion,
    };

    fetch('https://apartmain.azurewebsites.net/api/suggestions/deletesuggestion', {
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
          alert('Editing error!');
        }
        this.suggestion = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getSuggestions(): void {
    fetch(`https://apartmain.azurewebsites.net/api/suggestions/getsuggestions?page=${this.page}&pageSize=${this.pageSize}`, {
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
          this.suggestions = data.suggestions;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  collectElements(suggestions: Suggestion[]): void {
    for (let item of suggestions) {
      this.suggestions?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(`https://apartmain.azurewebsites.net/api/suggestions/getsuggestions?page=${this.page}&pageSize=${this.pageSize}`, {
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
          this.collectElements(data.suggestions);
          //this.suggestions = data.suggestions;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
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
