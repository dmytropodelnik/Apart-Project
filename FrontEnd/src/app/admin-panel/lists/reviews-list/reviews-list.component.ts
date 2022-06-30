import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Review } from 'src/app/models/Review/review.item';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-reviews-list',
  templateUrl: './reviews-list.component.html',
  styleUrls: ['./reviews-list.component.css'],
})
export class ReviewsListComponent implements OnInit {
  reviews: Review[] | null = null;
  review: Review | null = null;
  checkedReview: number | null = null;

  isEditEnabled: boolean = true;
  isDeleteEnabled: boolean = true;

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
          this.showAlert('Adding error!');
        }
        this.review = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
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
          this.showAlert('Editing error!');
        }
        this.review = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
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
          this.showAlert('Editing error!');
        }
        this.review = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  getReviews(): void {
    fetch(
      `https://apartmain.azurewebsites.net/api/reviews/getreviews?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.reviews = data.reviews;
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  collectElements(reviews: Review[]): void {
    for (let item of reviews) {
      this.reviews?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(
      `https://apartmain.azurewebsites.net/api/reviews/getreviews?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.collectElements(data.reviews);
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
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
