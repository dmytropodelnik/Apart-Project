import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CardType } from 'src/app/models/Payment/cardtype.item';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-card-types-list',
  templateUrl: './card-types-list.component.html',
  styleUrls: ['./card-types-list.component.css'],
})
export class CardTypesListComponent implements OnInit {
  cardTypes: CardType[] | null = null;
  type: string | null = null;
  checkedType: number | null = null;

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

  addType(): void {
    let type = {
      name: this.type,
    };

    fetch('https://localhost:44381/api/cardtypes/addtype', {
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
        this.type = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  editType(): void {
    let type = {
      id: this.checkedType,
      name: this.type,
    };

    fetch('https://localhost:44381/api/cardtypes/edittype', {
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
        this.type = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  deleteType(): void {
    let type = {
      id: this.checkedType,
      name: this.type,
    };

    fetch('https://localhost:44381/api/cardtypes/deletetype', {
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
        this.type = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  getTypes(): void {
    fetch('https://localhost:44381/api/cardtypes/gettypes', {
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
          this.cardTypes = data.cardTypes;
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
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
