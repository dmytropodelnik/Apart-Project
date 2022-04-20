import { Component, OnInit } from '@angular/core';
import { Review } from 'src/app/models/Review/review.item';
import { Gender } from 'src/app/models/UserData/gender.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-genders-list',
  templateUrl: './genders-list.component.html',
  styleUrls: ['./genders-list.component.css']
})
export class GendersListComponent implements OnInit {

  genders: Gender[] | null = null;
  gender: string | null = null;
  searchGender: string = '';
  checkedGender: number | null = null;

  isEditEnabled: boolean = true;
  isDeleteEnabled: boolean = true;

  constructor() {}

  search(): void {
    fetch('https://apartmain.azurewebsites.net/api/genders/search?gender=' + this.searchGender, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.genders = data.genders;
        } else {
          alert('Search error!');
        }
        this.searchGender = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addGender(): void {
    let gender = {
      title: this.gender,
    };

    fetch('https://apartmain.azurewebsites.net/api/genders/addgender', {
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

    fetch('https://apartmain.azurewebsites.net/api/genders/editgender', {
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
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
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

    fetch('https://apartmain.azurewebsites.net/api/genders/deletegender', {
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
          ListHelper.disableButtons();
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
    fetch('https://apartmain.azurewebsites.net/api/genders/getgenders', {
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
