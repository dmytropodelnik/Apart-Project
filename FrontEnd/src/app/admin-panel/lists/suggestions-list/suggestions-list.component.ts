import { Component, OnInit } from '@angular/core';
import { Suggestion } from 'src/app/models/Suggestions/suggestion.item';

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

  constructor() {}

  addSuggestion(): void {
    let suggestion = {
      name: this.suggestion,
    };

    fetch('https://localhost:44381/api/suggestions/addsuggestion', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
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

    fetch('https://localhost:44381/api/suggestions/editsuggestion', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
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

    fetch('https://localhost:44381/api/suggestions/deletesuggestion', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
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
    fetch('https://localhost:44381/api/suggestions/getsuggestions', {
      method: 'GET',
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
