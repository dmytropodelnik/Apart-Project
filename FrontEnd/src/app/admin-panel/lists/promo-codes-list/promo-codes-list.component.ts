import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PromoCode } from 'src/app/models/Payment/promocode.item';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-promo-codes-list',
  templateUrl: './promo-codes-list.component.html',
  styleUrls: ['./promo-codes-list.component.css'],
})
export class PromoCodesListComponent implements OnInit {
  codes: PromoCode[] | null = null;
  code: PromoCode | null = null;
  searchCode: string = '';
  checkedCode: number | null = null;

  page: number = 1;
  pageSize: number = 20;

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
<<<<<<< HEAD
    fetch('https://apartmain.azurewebsites.net/api/promocodes/search?code=' + this.searchCode, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
=======
    fetch(
      'https://localhost:44381/api/promocodes/search?code=' + this.searchCode,
      {
        method: 'GET',
        headers: {
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
>>>>>>> backend
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.codes = data.codes;
        } else {
          this.showAlert('Search error!');
        }
        this.searchCode = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
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
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(code),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCodes();
        } else {
          this.showAlert('Adding error!');
        }
        this.code = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
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
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(code),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCodes();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.code = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
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
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(code),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCodes();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.code = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  getCodes(): void {
<<<<<<< HEAD
    fetch(`https://apartmain.azurewebsites.net/api/promocodes/getcodes?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
=======
    fetch(
      `https://localhost:44381/api/promocodes/getcodes?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.codes = data.codes;
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  collectElements(codes: PromoCode[]): void {
    for (let item of codes) {
      this.codes?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

<<<<<<< HEAD
    fetch(`https://apartmain.azurewebsites.net/api/promocodes/getcodes?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
=======
    fetch(
      `https://localhost:44381/api/promocodes/getcodes?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.collectElements(data.codes);
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
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
