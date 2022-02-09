import { Component, OnInit } from '@angular/core';
import { Language } from 'src/app/models/language.item';

import AuthHelper from '../../../utils/authHelper';

@Component({
  selector: 'app-languages-list',
  templateUrl: './languages-list.component.html',
  styleUrls: ['./languages-list.component.css']
})
export class LanguagesListComponent implements OnInit {

  title: string = '';
  languages: Language[] | null = null;

  constructor() { }

  addLang(): void {
    let lang = {
      title: this.title,
    };

    fetch('https://localhost:44381/api/languages/addlanguage', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        "Accept": "application/json",
        "Authorization": "Bearer " + AuthHelper.getToken(),
       },
      body: JSON.stringify(lang),
    })
    .then((r) => r.json())
    .then((response) => {
        alert(response.code);
        console.log(response);

        if (response.code === 200) {
          alert("Language has been successfully added");
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editLang(): void {
    
  }

  deleteLang(): void {

  }

  ngOnInit(): void {
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

}
