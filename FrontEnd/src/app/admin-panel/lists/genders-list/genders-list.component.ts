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
  gender: string | null = null;
  checkedGender: number | null = null;

  isEditEnabled: boolean = true;
  isDeleteEnabled: boolean = true;

  constructor() {}

  addGender(): void {
    let gender = {
      title: this.gender,
    };

    fetch('https://localhost:44381/api/genders/addgender', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(gender),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getGenders();
        } else {
          alert('Adding error!');
        }
        this.gender = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editGender(): void {
    let gender = {
      id: this.checkedGender,
      title: this.gender,
    };

    fetch('https://localhost:44381/api/genders/editgender', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(gender),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getGenders();
        } else {
          alert('Editing error!');
        }
        console.log(data);
        this.gender = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteGender(): void {
    let gender = {
      id: this.checkedGender,
      title: this.gender,
    };

    fetch('https://localhost:44381/api/genders/deletegender', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(gender),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getGenders();
        } else {
          alert('Editing error!');
        }
        this.gender = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getGenders(): void {
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

  setGender(id: number | null, gender: string): void {
    this.checkedGender = id;
    this.gender = gender;
    
    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getGenders();
  }

}
