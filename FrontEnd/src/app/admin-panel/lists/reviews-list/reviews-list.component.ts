import { Component, OnInit } from '@angular/core';
import { Review } from 'src/app/models/Review/review.item';
import { AdminContentService } from 'src/app/services/admin-content.service';


import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-reviews-list',
  templateUrl: './reviews-list.component.html',
  styleUrls: ['./reviews-list.component.css']
})
export class ReviewsListComponent implements OnInit {

  reviews: Review[] | null = null;
  review: Review | null = null;
  checkedReview: number | null = null;

  isEditEnabled: boolean = true;
  isDeleteEnabled: boolean = true;

  page: number = 1;
  pageSize: number = 15;

  constructor(
    private adminContentService: AdminContentService
  ) {

  }

  addReview(): void {
    let review = {
      title: this.review,
    };

    fetch('https://apartmain.azurewebsites.net/api/reviews/addreview', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(review),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getReviews();
          ListHelper.disableButtons();
        } else {
          alert('Adding error!');
        }
        this.review = null;
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

    fetch('https://apartmain.azurewebsites.net/api/reviews/editreview', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(review),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getReviews();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.review = null;
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

    fetch('https://apartmain.azurewebsites.net/api/reviews/deletereview', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
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
        this.review = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getReviews(): void {
    fetch(`https://apartmain.azurewebsites.net/api/reviews/getreviews?page=${this.page}&pageSize=${this.pageSize}`, {
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
          this.reviews = data.reviews;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  collectElements(reviews: Review[]): void {
    for (let item of reviews) {
      this.reviews?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(`https://apartmain.azurewebsites.net/api/reviews/getreviews?page=${this.page}&pageSize=${this.pageSize}`, {
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
          this.collectElements(data.reviews);
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setReview(review: Review): void {
    this.checkedReview = review.id;
    this.review = review;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getReviews();
  }
}
