import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SuggestionHighlight } from 'src/app/models/Suggestions/suggestionhighlight.item';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-suggestion-highlights-list',
  templateUrl: './suggestion-highlights-list.component.html',
  styleUrls: ['./suggestion-highlights-list.component.css'],
})
export class SuggestionHighlightsListComponent implements OnInit {
  highlights: SuggestionHighlight[] | null = null;
  highlight: string | null = null;
  searchHighlight: string = '';
  checkedHighlight: number | null = null;

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

  addHighlight(): void {
    let highlight = {
      name: this.highlight,
    };

    fetch('https://apartmain.azurewebsites.net/api/suggestionhighlights/addhighlight', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(highlight),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getHighlights();
        } else {
          this.showAlert('Adding error!');
        }
        this.highlight = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  editHighlight(): void {
    let highlight = {
      id: this.checkedHighlight,
      name: this.highlight,
    };

    fetch('https://apartmain.azurewebsites.net/api/suggestionhighlights/edithighlight', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(highlight),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getHighlights();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.highlight = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  deleteHighlight(): void {
    let highlight = {
      id: this.checkedHighlight,
      name: this.highlight,
    };

    fetch('https://apartmain.azurewebsites.net/api/suggestionhighlights/deletehighlight', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(highlight),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getHighlights();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.highlight = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  getHighlights(): void {
<<<<<<< HEAD
    fetch(`https://apartmain.azurewebsites.net/api/suggestionhighlights/gethighlights?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
=======
    fetch(
      `https://localhost:44381/api/suggestionhighlights/gethighlights?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.highlights = data.highlights;
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  collectElements(highlights: SuggestionHighlight[]): void {
    for (let item of highlights) {
      this.highlights?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

<<<<<<< HEAD
    fetch(`https://apartmain.azurewebsites.net/api/suggestionhighlights/gethighlights?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
=======
    fetch(
      `https://localhost:44381/api/suggestionhighlights/gethighlights?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.collectElements(data.highlights);
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
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
