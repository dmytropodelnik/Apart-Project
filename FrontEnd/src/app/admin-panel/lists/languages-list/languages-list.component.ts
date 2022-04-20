import { Component, OnInit } from '@angular/core';
import { Language } from 'src/app/models/language.item';
import { AdminContentService } from 'src/app/services/admin-content.service';

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

  constructor(
    private adminContentService: AdminContentService
  ) {

  }

  search(): void {
    fetch('https://apartmain.azurewebsites.net/api/languages/search?lang=' + this.searchLang, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.languages = data.languages;
        } else {
          alert('Search error!');
        }
        this.searchLang = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addLang(): void {
    let lang = {
      title: this.lang,
    };

    fetch('https://apartmain.azurewebsites.net/api/languages/addlanguage', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(lang),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getLangs();
        } else {
          alert('Adding error!');
        }
        this.lang = '';
      })
      .catch((ex) => {
        alert(ex);
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
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(lang),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getLangs();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        console.log(data);
        this.lang = '';
      })
      .catch((ex) => {
        alert(ex);
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
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(lang),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getLangs();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.lang = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  collectElements(languages: Language[]): void {
    for (let item of languages) {
      this.languages?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(`https://apartmain.azurewebsites.net/api/languages/getlanguages?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.languages);
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setLang(id: number | null, lang: string): void {
    this.checkedLang = id;
    this.lang = lang;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  getLangs(): void {
    fetch(`https://apartmain.azurewebsites.net/api/languages/getlanguages?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.languages = data.languages;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  ngOnInit(): void {
    this.getLangs();
  }
}
