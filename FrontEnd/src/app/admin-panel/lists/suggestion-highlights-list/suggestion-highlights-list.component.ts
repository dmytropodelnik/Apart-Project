import { Component, OnInit } from '@angular/core';
import { SuggestionHighlight } from 'src/app/models/Suggestions/suggestionhighlight.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-suggestion-highlights-list',
  templateUrl: './suggestion-highlights-list.component.html',
  styleUrls: ['./suggestion-highlights-list.component.css']
})
export class SuggestionHighlightsListComponent implements OnInit {

  highlights: SuggestionHighlight[] | null = null;
  highlight: string | null = null;
  searchHighlight: string = '';
  checkedHighlight: number | null = null;

  constructor() {}

  addHighlight(): void {
    let highlight = {
      name: this.highlight,
    };

    fetch('https://localhost:44381/api/suggestionhighlights/addhighlight', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(highlight),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getHighlights();
        } else {
          alert('Adding error!');
        }
        this.highlight = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editHighlight(): void {
    let highlight = {
      id: this.checkedHighlight,
      name: this.highlight,
    };

    fetch('https://localhost:44381/api/suggestionhighlights/edithighlight', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(highlight),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getHighlights();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.highlight = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteHighlight(): void {
    let highlight = {
      id: this.checkedHighlight,
      name: this.highlight,
    };

    fetch('https://localhost:44381/api/suggestionhighlights/deletehighlight', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(highlight),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getHighlights();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.highlight = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getHighlights(): void {
    fetch('https://localhost:44381/api/suggestionhighlights/gethighlights', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.highlights = data.highlights;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setHighlight(id: number | null, highlight: string): void {
    this.checkedHighlight = id;
    this.highlight = highlight;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getHighlights();
  }

}
