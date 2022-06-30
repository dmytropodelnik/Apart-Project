import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Review } from 'src/app/models/Review/review.item';
import { Gender } from 'src/app/models/UserData/gender.item';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-genders-list',
  templateUrl: './genders-list.component.html',
  styleUrls: ['./genders-list.component.css'],
})
export class GendersListComponent implements OnInit {
  genders: Gender[] | null = null;
  gender: string | null = null;
  searchGender: string = '';
  checkedGender: number | null = null;

  isEditEnabled: boolean = true;
  isDeleteEnabled: boolean = true;

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
    fetch(
      'https://localhost:44381/api/genders/search?gender=' + this.searchGender,
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
          this.genders = data.genders;
        } else {
          this.showAlert('Search error!');
        }
        this.searchGender = '';
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  addGender(): void {
    let gender = {
      title: this.gender,
    };

    fetch('https://localhost:44381/api/genders/addgender', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(gender),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getGenders();
        } else {
          this.showAlert('Adding error!');
        }
        this.gender = '';
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  editGender(): void {
    let gender = {
      id: this.checkedGender,
      title: this.gender,
    };

    fetch('https://localhost:44381/api/genders/editgender', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(gender),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getGenders();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.gender = '';
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  deleteGender(): void {
    let gender = {
      id: this.checkedGender,
      title: this.gender,
    };

    fetch('https://localhost:44381/api/genders/deletegender', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(gender),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getGenders();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.gender = '';
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  getGenders(): void {
    fetch('https://localhost:44381/api/genders/getgenders', {
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
          this.genders = data.genders;
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  setGender(id: number | null, gender: string): void {
    this.checkedGender = id;
    this.gender = gender;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getGenders();
  }
}
