import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Role } from 'src/app/models/UserData/role.item';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-roles-list',
  templateUrl: './roles-list.component.html',
  styleUrls: ['./roles-list.component.css'],
})
export class RolesListComponent implements OnInit {
  roles: Role[] | null = null;
  role: string | null = null;
  searchRole: string = '';
  checkedRole: number | null = null;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  search(): void {
    fetch('https://apartmain.azurewebsites.net/api/roles/search?role=' + this.searchRole, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.roles = data.roles;
        } else {
          this.showAlert('Search error!');
        }
        this.searchRole = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  addRole(): void {
    let role = {
      name: this.role,
    };

    fetch('https://apartmain.azurewebsites.net/api/roles/addrole', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(role),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getRoles();
        } else {
          this.showAlert('Adding error!');
        }
        this.role = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  editRole(): void {
    let role = {
      id: this.checkedRole,
      name: this.role,
    };

    fetch('https://apartmain.azurewebsites.net/api/roles/editrole', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(role),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getRoles();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.role = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  deleteRole(): void {
    let role = {
      id: this.checkedRole,
      name: this.role,
    };

    fetch('https://apartmain.azurewebsites.net/api/roles/deleterole', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(role),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getRoles();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.role = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  getRoles(): void {
    fetch('https://apartmain.azurewebsites.net/api/roles/getroles', {
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
          this.roles = data.roles;
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  setRole(id: number | null, role: string): void {
    this.checkedRole = id;
    this.role = role;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getRoles();
  }
}
