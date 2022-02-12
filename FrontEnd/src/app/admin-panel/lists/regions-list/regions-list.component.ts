import { Component, OnInit } from '@angular/core';
import { Region } from 'src/app/models/Location/region.item';

import AuthHelper from '../../../utils/authHelper';


@Component({
  selector: 'app-regions-list',
  templateUrl: './regions-list.component.html',
  styleUrls: ['./regions-list.component.css']
})
export class RegionsListComponent implements OnInit {

  regions: Region[] | null = null;
  region: string | null = null;
  checkedRegion: number | null = null;

  constructor() {}

  addRegion(): void {
    let region = {
      name: this.region,
    };

    fetch('https://localhost:44381/api/roles/addrole', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(region),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getRegions();
        } else {
          alert('Adding error!');
        }
        this.region = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editRegion(): void {
    let region = {
      id: this.checkedRegion,
      name: this.region,
    };

    fetch('https://localhost:44381/api/regions/editregion', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(region),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getRegions();
        } else {
          alert('Editing error!');
        }
        console.log(data);
        this.region = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteRegion(): void {
    let region = {
      id: this.checkedRegion,
      name: this.region,
    };

    fetch('https://localhost:44381/api/roles/deleterole', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(region),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getRegions();
        } else {
          alert('Editing error!');
        }
        this.region = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getRegions(): void {
    fetch('https://localhost:44381/api/regions/getregions', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.regions = data.regions;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setRegion(id: number | null, region: string): void {
    this.checkedRegion = id;
    this.region = region;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getRegions();
  }

}
