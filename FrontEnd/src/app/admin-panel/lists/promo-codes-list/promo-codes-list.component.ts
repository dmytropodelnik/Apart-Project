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

  search(): void {
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
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  addCode(): void {
    let code = {
      name: this.code,
    };

    fetch('https://localhost:44381/api/promocodes/addcode', {
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
          alert('Adding error!');
        }
        this.code = null;
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  editCode(): void {
    let code = {
      id: this.checkedCode,
      name: this.code,
    };

    fetch('https://localhost:44381/api/promocodes/editcode', {
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
          alert('Editing error!');
        }
        this.code = null;
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  deleteCode(): void {
    let code = {
      id: this.checkedCode,
      name: this.code,
    };

    fetch('https://localhost:44381/api/promocodes/deletecode', {
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
          alert('Editing error!');
        }
        this.code = null;
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  getCodes(): void {
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
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.codes = data.codes;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  collectElements(codes: PromoCode[]): void {
    for (let item of codes) {
      this.codes?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

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
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.codes);
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
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
