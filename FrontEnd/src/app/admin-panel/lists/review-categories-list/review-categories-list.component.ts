import { Component, OnInit } from '@angular/core';
import { ReviewCategory } from 'src/app/models/Review/reviewcategory.item';

@Component({
  selector: 'app-review-categories-list',
  templateUrl: './review-categories-list.component.html',
  styleUrls: ['./review-categories-list.component.css']
})
export class ReviewCategoriesListComponent implements OnInit {

  categories: ReviewCategory[] | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}
