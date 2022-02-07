import { Component, OnInit } from '@angular/core';
import { Review } from 'src/app/models/Review/review.item';

@Component({
  selector: 'app-reviews-list',
  templateUrl: './reviews-list.component.html',
  styleUrls: ['./reviews-list.component.css']
})
export class ReviewsListComponent implements OnInit {

  reviews: Review[] | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}
