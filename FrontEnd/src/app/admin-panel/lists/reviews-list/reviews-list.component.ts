import { Component, OnInit } from '@angular/core';
import { Review } from 'src/app/models/Review/review.item';


import AuthHelper from '../../../utils/authHelper';

@Component({
  selector: 'app-reviews-list',
  templateUrl: './reviews-list.component.html',
  styleUrls: ['./reviews-list.component.css']
})
export class ReviewsListComponent implements OnInit {

  reviews: Review[] | null = null;
  review: string | null = null;
  checkedReview: number | null = null;

  isEditEnabled: boolean = true;
  isDeleteEnabled: boolean = true;

  constructor() { }

  addReview(): void {
    let review = {
      title: this.review,
    };

    fetch('https://localhost:44381/api/reviews/addreview', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(review),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getReviews();
        } else {
          alert('Adding error!');
        }
        this.review = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editReview(): void {
    let review = {
      id: this.checkedReview,
      title: this.review,
    };

    fetch('https://localhost:44381/api/reviews/editreview', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(review),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getReviews();
        } else {
          alert('Editing error!');
        }
        this.review = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteReview(): void {
    let review = {
      id: this.checkedReview,
      title: this.review,
    };

    fetch('https://localhost:44381/api/reviews/deletereview', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(review),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getReviews();
        } else {
          alert('Editing error!');
        }
        this.review = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getReviews(): void {
    fetch('https://localhost:44381/api/reviews/getreviews', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.reviews = data.reviews;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setReview(id: number | null, review: string): void {
    this.checkedReview = id;
    this.review = review;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getReviews();
  }
}
