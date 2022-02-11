import { Component, OnInit } from '@angular/core';
import { SuggestionReviewGrade } from 'src/app/models/Suggestions/suggestionreviewgrade.item';

import AuthHelper from '../../../utils/authHelper';

@Component({
  selector: 'app-suggestion-review-grades-list',
  templateUrl: './suggestion-review-grades-list.component.html',
  styleUrls: ['./suggestion-review-grades-list.component.css']
})
export class SuggestionReviewGradesListComponent implements OnInit {

  grades: SuggestionReviewGrade[] | null = null;
  grade: string | null = null;
  checkedGrade: number | null = null;

  constructor() {}

  addGrade(): void {
    let grade = {
      name: this.grade,
    };

    fetch('https://localhost:44381/api/grades/addgrade', {
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
        this.grade = '';
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

    fetch('https://localhost:44381/api/grades/editgrade', {
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
        } else {
          alert('Editing error!');
        }
        this.grade = '';
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

    fetch('https://localhost:44381/api/grades/deletegrade', {
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
          alert('Editing error!');
        }
        this.grade = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getGrades(): void {
    fetch('https://localhost:44381/api/grades/getgrades', {
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

  setGrade(id: number | null, grade: string): void {
    this.checkedGrade = id;
    this.grade = grade;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getGrades();
  }

}
