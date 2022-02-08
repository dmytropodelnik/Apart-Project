import { Component, OnInit } from '@angular/core';
import { SuggestionReviewGrade } from 'src/app/models/Suggestions/suggestionreviewgrade.item';

@Component({
  selector: 'app-suggestion-review-grades-list',
  templateUrl: './suggestion-review-grades-list.component.html',
  styleUrls: ['./suggestion-review-grades-list.component.css']
})
export class SuggestionReviewGradesListComponent implements OnInit {

  grades: SuggestionReviewGrade[] | null = null;

  constructor() { }

  ngOnInit(): void {
    fetch('https://localhost:44381/api/countries/getcountries', {
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

}
