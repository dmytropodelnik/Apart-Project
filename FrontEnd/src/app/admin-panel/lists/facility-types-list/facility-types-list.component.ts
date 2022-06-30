import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FacilityType } from 'src/app/models/facilitytype.item';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-facility-types-list',
  templateUrl: './facility-types-list.component.html',
  styleUrls: ['./facility-types-list.component.css'],
})
export class FacilityTypesListComponent implements OnInit {
  types: FacilityType[] | null = null;
  type: FacilityType;
  searchType: string = '';
  checkedType: number | null = null;

  page: number = 1;
  pageSize: number = 15;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private adminContentService: AdminContentService,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {
    this.type = new FacilityType();
  }

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  search(): void {
    fetch(
      'https://apartmain.azurewebsites.net/api/facilitytypes/search?type=' +
        this.searchType,
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
          this.types = data.types;
        } else {
          this.showAlert('Search error!');
        }
        this.searchType = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  addType(): void {
    let type = {
      type: this.type.type,
    };

    fetch('https://apartmain.azurewebsites.net/api/facilitytypes/addtype', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(type),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getTypes();
        } else {
          this.showAlert('Adding error!');
        }
        this.type.type = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  editType(): void {
    let type = {
      id: this.checkedType,
      type: this.type.type,
    };

    fetch('https://apartmain.azurewebsites.net/api/facilitytypes/edittype', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(type),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getTypes();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.type.type = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  deleteType(): void {
    let type = {
      id: this.checkedType,
      type: this.type.type,
    };

    fetch('https://apartmain.azurewebsites.net/api/facilitytypes/deletetype', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(type),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getTypes();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.type.type = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  getTypes(): void {
    fetch(
      `https://apartmain.azurewebsites.net/api/facilitytypes/gettypes?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.types = data.types;
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  collectElements(types: FacilityType[]): void {
    for (let item of types) {
      this.types?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(
      `https://apartmain.azurewebsites.net/api/facilitytypes/gettypes?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.collectElements(data.types);
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  setType(type: FacilityType): void {
    this.checkedType = type.id;
    this.type.type = type.type;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getTypes();
  }
}
