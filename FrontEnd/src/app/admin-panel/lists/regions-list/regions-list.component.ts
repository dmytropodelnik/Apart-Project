import { Component, OnInit } from '@angular/core';
import { Region } from 'src/app/models/Location/region.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';
import ImageHelper from '../../../utils/imageHelper';
import { AdminContentService } from 'src/app/services/admin-content.service';

@Component({
  selector: 'app-regions-list',
  templateUrl: './regions-list.component.html',
  styleUrls: ['./regions-list.component.css']
})
export class RegionsListComponent implements OnInit {

  regions: Region[] | null = null;
  region: Region | null = null;
  searchRegion: string = '';
  checkedRegion: number | null = null;
  imageHelper: any = ImageHelper;

  page: number = 1;
  pageSize: number = 15;

  constructor(
    private adminContentService: AdminContentService
  ) {

  }

  search(): void {
    fetch('https://apartmain.azurewebsites.net/api/regions/search?region=' + this.searchRegion, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.regions = data.regions;
        } else {
          alert('Search error!');
        }
        this.searchRegion = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addRegion(): void {
    let region = {
      name: this.region,
    };

    fetch('https://apartmain.azurewebsites.net/api/regions/addregion', {
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
        this.region = null;
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

    fetch('https://apartmain.azurewebsites.net/api/regions/editregion', {
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
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.region = null;
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

    fetch('https://apartmain.azurewebsites.net/api/regions/deleteregion', {
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
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.region = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getRegions(): void {
    fetch(`https://apartmain.azurewebsites.net/api/regions/getregions?page=${this.page}&pageSize=${this.pageSize}`, {
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

  collectElements(regions: Region[]): void {
    for (let item of regions) {
      this.regions?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(`https://apartmain.azurewebsites.net/api/regions/getregions?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.regions);
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setRegion(region: Region): void {
    this.checkedRegion = region.id;
    this.region = region;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getRegions();
  }

}
