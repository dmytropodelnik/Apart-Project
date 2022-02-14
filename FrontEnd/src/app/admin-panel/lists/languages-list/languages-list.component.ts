import { Component, OnInit } from '@angular/core';
import { Language } from 'src/app/models/language.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-languages-list',
  templateUrl: './languages-list.component.html',
  styleUrls: ['./languages-list.component.css'],
})
export class LanguagesListComponent implements OnInit {
  title: string = '';
  languages: Language[] | null = null;
  lang: string | null = null;
  checkedLang: number | null = null;

  constructor() {}

  addLang(): void {
    let lang = {
      title: this.lang,
    };

    fetch('https://localhost:44381/api/languages/addlanguage', {
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

    fetch('https://localhost:44381/api/languages/editlanguage', {
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

    fetch('https://localhost:44381/api/languages/deletelang', {
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

  setLang(id: number | null, lang: string): void {
    this.checkedLang = id;
    this.lang = lang;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  getLangs(): void {
    fetch('https://localhost:44381/api/languages/getlanguages', {
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
