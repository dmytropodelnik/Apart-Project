import { Component, OnInit } from '@angular/core';
import { SurroundingObjectType } from 'src/app/models/Suggestions/surroundingobjecttype.item';
import { AdminContentService } from 'src/app/services/admin-content.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-surrounding-object-types-list',
  templateUrl: './surrounding-object-types-list.component.html',
  styleUrls: ['./surrounding-object-types-list.component.css']
})
export class SurroundingObjectTypesListComponent implements OnInit {

  types: SurroundingObjectType[] | null = null;
  type: string | null = null;
  searchType: string = '';
  checkedType: number | null = null;

  page: number = 1;
  pageSize: number = 15;

  constructor(
    private adminContentService: AdminContentService
  ) {

  }

  search(): void {
    fetch('https://apartmain.azurewebsites.net/api/surroundingobjecttypes/search?type=' + this.searchType, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.types = data.types;
        } else {
          alert('Search error!');
        }
        this.searchType = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addType(): void {
    let type = {
      name: this.type,
    };

    fetch('https://apartmain.azurewebsites.net/api/surroundingobjecttypes/addtype', {
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
          alert('Adding error!');
        }
        this.type = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editType(): void {
    let type = {
      id: this.checkedType,
      name: this.type,
    };

    fetch('https://apartmain.azurewebsites.net/api/surroundingobjecttypes/edittype', {
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
          alert('Editing error!');
        }
        this.type = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteType(): void {
    let type = {
      id: this.checkedType,
      name: this.type,
    };

    fetch('https://apartmain.azurewebsites.net/api/surroundingobjecttypes/deletetype', {
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
          alert('Editing error!');
        }
        this.type = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getTypes(): void {
    fetch(`https://apartmain.azurewebsites.net/api/surroundingobjecttypes/gettypes?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.types = data.types;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  collectElements(types: SurroundingObjectType[]): void {
    for (let item of types) {
      this.types?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(`https://apartmain.azurewebsites.net/api/surroundingobjecttypes/gettypes?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.types);
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setType(id: number | null, type: string): void {
    this.checkedType = id;
    this.type = type;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getTypes();
  }

}
