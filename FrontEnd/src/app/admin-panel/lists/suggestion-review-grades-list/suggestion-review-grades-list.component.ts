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
  }

}
