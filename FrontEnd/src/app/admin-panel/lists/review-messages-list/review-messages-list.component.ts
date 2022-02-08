import { Component, OnInit } from '@angular/core';
import { ReviewMessage } from 'src/app/models/Review/reviewmessage.item';

@Component({
  selector: 'app-review-messages-list',
  templateUrl: './review-messages-list.component.html',
  styleUrls: ['./review-messages-list.component.css']
})
export class ReviewMessagesListComponent implements OnInit {

  reviewMessages: ReviewMessage[] | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}