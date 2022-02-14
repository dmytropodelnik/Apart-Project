import { Component, OnInit } from '@angular/core';
import { PromoCode } from 'src/app/models/Payment/promocode.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-promo-codes-list',
  templateUrl: './promo-codes-list.component.html',
  styleUrls: ['./promo-codes-list.component.css']
})
export class PromoCodesListComponent implements OnInit {

  codes: PromoCode[] | null = null;
  code: string | null = null;
  checkedCode: number | null = null;

  constructor() {}

  addCode(): void {
    let code = {
      name: this.code,
    };

    fetch('https://localhost:44381/api/codes/addcode', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(code),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCodes();
        } else {
          alert('Adding error!');
        }
        this.code = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editCode(): void {
    let code = {
      id: this.checkedCode,
      name: this.code,
    };

    fetch('https://localhost:44381/api/codes/editcode', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(code),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCodes();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.code = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteCode(): void {
    let code = {
      id: this.checkedCode,
      name: this.code,
    };

    fetch('https://localhost:44381/api/codes/deletecode', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(code),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCodes();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.code = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getCodes(): void {
    fetch('https://localhost:44381/api/codes/getcodes', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.codes = data.codes;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setCode(id: number | null, code: string): void {
    this.checkedCode = id;
    this.code = code;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getCodes();
  }

}
