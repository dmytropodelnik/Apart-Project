import { Component, OnInit } from '@angular/core';
import { Airport } from 'src/app/models/Location/airport.item';
import { AdminContentService } from 'src/app/services/admin-content.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-airports-list',
  templateUrl: './airports-list.component.html',
  styleUrls: ['./airports-list.component.css']
})
export class AirportsListComponent implements OnInit {

  airports: Airport[] | null = null;
  airport: string | null = null;
  checkedAirport: number | null = null;

  page: number = 1;
  pageSize: number = 10;

  constructor(
    private adminContentService: AdminContentService
  ) {

  }

  addAirport(): void {
    let airport = {
      name: this.airport,
    };

    fetch('https://localhost:44381/api/airports/addairport', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(airport),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getAirports();
        } else {
          alert('Adding error!');
        }
        this.airport = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editAirport(): void {
    let airport = {
      id: this.checkedAirport,
      name: this.airport,
    };

    fetch('https://localhost:44381/api/airports/editairport', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(airport),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getAirports();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.airport = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteAirport(): void {
    let airport = {
      id: this.checkedAirport,
      name: this.airport,
    };

    fetch('https://localhost:44381/api/airports/deleteairport', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(airport),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getAirports();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.airport = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getAirports(): void {
    fetch(`https://localhost:44381/api/airports/getairports?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.airports = data.airports;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  collectElements(airports: Airport[]): void {
    for (let item of airports) {
      this.airports?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(`https://localhost:44381/api/airports/getairports?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.airports);
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setAirport(id: number | null, airport: string): void {
    this.checkedAirport = id;
    this.airport = airport;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getAirports();
  }

}
