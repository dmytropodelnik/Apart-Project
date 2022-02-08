import { Component, OnInit } from '@angular/core';
import { Role } from 'src/app/models/UserData/role.item';

@Component({
  selector: 'app-roles-list',
  templateUrl: './roles-list.component.html',
  styleUrls: ['./roles-list.component.css']
})
export class RolesListComponent implements OnInit {

  roles: Role[] | null = null;

  constructor() { }

  ngOnInit(): void {
    fetch('https://localhost:44381/api/roles/getroles', {
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

}
