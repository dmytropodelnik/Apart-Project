import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SuggestionReviewGrade } from 'src/app/models/Suggestions/suggestionreviewgrade.item';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-suggestion-review-grades-list',
  templateUrl: './suggestion-review-grades-list.component.html',
  styleUrls: ['./suggestion-review-grades-list.component.css'],
})
export class SuggestionReviewGradesListComponent implements OnInit {
  grades: SuggestionReviewGrade[] | null = null;
  grade: number | null = null;
  checkedGrade: number | null = null;

  page: number = 1;
  pageSize: number = 15;

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

  addGrade(): void {
    let grade = {
      name: this.grade,
    };

    fetch('https://localhost:44381/api/suggestionreviewgrades/addgrade', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(grade),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getGrades();
        } else {
          this.showAlert('Adding error!');
        }
        this.grade = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  editGrade(): void {
    let grade = {
      id: this.checkedGrade,
      name: this.grade,
    };

    fetch('https://localhost:44381/api/suggestionreviewgrades/editgrade', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(grade),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getGrades();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.grade = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  deleteGrade(): void {
    let grade = {
      id: this.checkedGrade,
      name: this.grade,
    };

    fetch('https://localhost:44381/api/suggestionreviewgrades/deletegrade', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(grade),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getGrades();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.grade = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  getGrades(): void {
    fetch(
      `https://localhost:44381/api/suggestionreviewgrades/getgrades?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.grades = data.grades;
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  collectElements(grades: SuggestionReviewGrade[]): void {
    for (let item of grades) {
      this.grades?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(
      `https://localhost:44381/api/suggestionreviewgrades/getgrades?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.collectElements(data.grades);
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  setGrade(id: number | null, grade: number | null): void {
    this.checkedGrade = id;
    this.grade = grade;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getGrades();
  }
}
