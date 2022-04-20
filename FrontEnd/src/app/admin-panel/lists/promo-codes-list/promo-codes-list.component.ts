import { Component, OnInit } from '@angular/core';
import { PromoCode } from 'src/app/models/Payment/promocode.item';
import { AdminContentService } from 'src/app/services/admin-content.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-promo-codes-list',
  templateUrl: './promo-codes-list.component.html',
  styleUrls: ['./promo-codes-list.component.css']
})
export class PromoCodesListComponent implements OnInit {

  codes: PromoCode[] | null = null;
  code: PromoCode | null = null;
  searchCode: string = '';
  checkedCode: number | null = null;

  page: number = 1;
  pageSize: number = 20;

  constructor(
    private adminContentService: AdminContentService
  ) {

  }

  search(): void {
    fetch('https://apartmain.azurewebsites.net/api/promocodes/search?code=' + this.searchCode, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.codes = data.codes;
        } else {
          alert('Search error!');
        }
        this.searchCode = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addCode(): void {
    let code = {
      name: this.code,
    };

    fetch('https://apartmain.azurewebsites.net/api/promocodes/addcode', {
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
        this.code = null;
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

    fetch('https://apartmain.azurewebsites.net/api/promocodes/editcode', {
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
        this.code = null;
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

    fetch('https://apartmain.azurewebsites.net/api/promocodes/deletecode', {
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
        this.code = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getCodes(): void {
    fetch(`https://apartmain.azurewebsites.net/api/promocodes/getcodes?page=${this.page}&pageSize=${this.pageSize}`, {
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

  collectElements(codes: PromoCode[]): void {
    for (let item of codes) {
      this.codes?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(`https://apartmain.azurewebsites.net/api/promocodes/getcodes?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.codes);
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setCode(code: PromoCode): void {
    this.checkedCode = code.id;
    this.code = code;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getCodes();
  }

}
