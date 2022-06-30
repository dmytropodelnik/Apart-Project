import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Language } from 'src/app/models/language.item';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-languages-list',
  templateUrl: './languages-list.component.html',
  styleUrls: ['./languages-list.component.css'],
})
export class LanguagesListComponent implements OnInit {
  languages: Language[] | null = null;
  lang: string | null = null;
  searchLang: string = '';
  checkedLang: number | null = null;

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
    fetch(
      'https://apartmain.azurewebsites.net/api/languages/search?lang=' +
        this.searchLang,
      {
        method: 'GET',
        headers: {
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.languages = data.languages;
        } else {
          this.showAlert('Search error!');
        }
        this.searchLang = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  addLang(): void {
    let lang = {
      title: this.lang,
    };

    fetch('https://apartmain.azurewebsites.net/api/languages/addlanguage', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(lang),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getLangs();
        } else {
          this.showAlert('Adding error!');
        }
        this.lang = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  editLang(): void {
    let lang = {
      id: this.checkedLang,
      title: this.lang,
    };

    fetch('https://apartmain.azurewebsites.net/api/languages/editlanguage', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(lang),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getLangs();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.lang = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  deleteLang(): void {
    let lang = {
      id: this.checkedLang,
      title: this.lang,
    };

    fetch('https://apartmain.azurewebsites.net/api/languages/deletelang', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(lang),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getLangs();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.lang = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  collectElements(languages: Language[]): void {
    for (let item of languages) {
      this.languages?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(
      `https://apartmain.azurewebsites.net/api/languages/getlanguages?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.collectElements(data.languages);
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  setLang(id: number | null, lang: string): void {
    this.checkedLang = id;
    this.lang = lang;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  getLangs(): void {
    fetch(
      `https://apartmain.azurewebsites.net/api/languages/getlanguages?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.languages = data.languages;
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  ngOnInit(): void {
    this.getLangs();
  }
}
