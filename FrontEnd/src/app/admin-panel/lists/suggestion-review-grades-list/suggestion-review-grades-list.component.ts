import { Component, OnInit } from '@angular/core';
import { SuggestionReviewGrade } from 'src/app/models/Suggestions/suggestionreviewgrade.item';
import { AdminContentService } from 'src/app/services/admin-content.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-suggestion-review-grades-list',
  templateUrl: './suggestion-review-grades-list.component.html',
  styleUrls: ['./suggestion-review-grades-list.component.css']
})
export class SuggestionReviewGradesListComponent implements OnInit {

  grades: SuggestionReviewGrade[] | null = null;
  grade: number | null = null;
  checkedGrade: number | null = null;

  page: number = 1;
  pageSize: number = 15;

  constructor(
    private adminContentService: AdminContentService
  ) {

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
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(grade),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getGrades();
        } else {
          alert('Adding error!');
        }
        this.grade = null;
      })
      .catch((ex) => {
        alert(ex);
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
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(grade),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getGrades();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.grade = null;
      })
      .catch((ex) => {
        alert(ex);
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
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(grade),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getGrades();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.grade = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getGrades(): void {
    fetch(`https://localhost:44381/api/suggestionreviewgrades/getgrades?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.grades = data.grades;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  collectElements(grades: SuggestionReviewGrade[]): void {
    for (let item of grades) {
      this.grades?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(`https://localhost:44381/api/suggestionreviewgrades/getgrades?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.grades);
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
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
