import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Airport } from 'src/app/models/Location/airport.item';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-airports-list',
  templateUrl: './airports-list.component.html',
  styleUrls: ['./airports-list.component.css'],
})
export class AirportsListComponent implements OnInit {
  airports: Airport[] | null = null;
  airport: string | null = null;
  checkedAirport: number | null = null;

  page: number = 1;
  pageSize: number = 10;

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

  addAirport(): void {
    let airport = {
      name: this.airport,
    };

    fetch('https://apartmain.azurewebsites.net/api/airports/addairport', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(airport),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getAirports();
        } else {
          this.showAlert('Adding error!');
        }
        this.airport = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  editAirport(): void {
    let airport = {
      id: this.checkedAirport,
      name: this.airport,
    };

    fetch('https://apartmain.azurewebsites.net/api/airports/editairport', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(airport),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getAirports();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.airport = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  deleteAirport(): void {
    let airport = {
      id: this.checkedAirport,
      name: this.airport,
    };

    fetch('https://apartmain.azurewebsites.net/api/airports/deleteairport', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(airport),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getAirports();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.airport = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  getAirports(): void {
    fetch(
      `https://apartmain.azurewebsites.net/api/airports/getairports?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.airports = data.airports;
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  collectElements(airports: Airport[]): void {
    for (let item of airports) {
      this.airports?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(
      `https://apartmain.azurewebsites.net/api/airports/getairports?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.collectElements(data.airports);
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
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
