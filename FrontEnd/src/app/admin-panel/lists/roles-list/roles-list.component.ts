import { Component, OnInit } from '@angular/core';
import { Role } from 'src/app/models/UserData/role.item';

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

  constructor() {}

  search(): void {
    fetch('http://apartmain.azurewebsites.net/api/roles/search?role=' + this.searchRole, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.roles = data.roles;
        } else {
          alert('Search error!');
        }
        this.searchRole = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addRole(): void {
    let role = {
      name: this.role,
    };

    fetch('http://apartmain.azurewebsites.net/api/roles/addrole', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(role),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getRoles();
        } else {
          alert('Adding error!');
        }
        this.role = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editRole(): void {
    let role = {
      id: this.checkedRole,
      name: this.role,
    };

    fetch('http://apartmain.azurewebsites.net/api/roles/editrole', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(role),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getRoles();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        console.log(data);
        this.role = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteRole(): void {
    let role = {
      id: this.checkedRole,
      name: this.role,
    };

    fetch('http://apartmain.azurewebsites.net/api/roles/deleterole', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(role),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getRoles();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        console.log(role);
        this.role = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getRoles(): void {
    fetch('http://apartmain.azurewebsites.net/api/roles/getroles', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.roles = data.roles;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
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
