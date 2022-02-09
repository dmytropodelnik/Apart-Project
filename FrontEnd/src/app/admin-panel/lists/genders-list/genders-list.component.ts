import { Component, OnInit } from '@angular/core';
import { Gender } from 'src/app/models/UserData/gender.item';

import AuthHelper from '../../../utils/authHelper';

@Component({
  selector: 'app-genders-list',
  templateUrl: './genders-list.component.html',
  styleUrls: ['./genders-list.component.css']
})
export class GendersListComponent implements OnInit {

  genders: Gender[] | null = null;

  constructor() { }

  ngOnInit(): void {
    fetch('https://localhost:44381/api/genders/getgenders', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.genders = data.genders;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

}
