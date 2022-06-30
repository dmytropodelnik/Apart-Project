import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthorizationService } from '../services/authorization.service';
import { MainDataService } from '../services/main-data.service';

import AuthHelper from '../utils/authHelper';
import ImageHelper from '../utils/imageHelper';
import MathHelper from '../utils/mathHelper';

@Component({
  selector: 'app-users-reviews',
  templateUrl: './users-reviews.component.html',
  styleUrls: ['./users-reviews.component.css'],
})
export class UsersReviewsComponent implements OnInit {
  usersReviews: any[] = [];
  usersPropertiesReviews: any[] = [];

  imageHelper: any = ImageHelper;
  mathHelper: any = MathHelper;

  userCountryImage: string = '';
  userFirstName: string = '';
  userLastName: string = '';

  userReviewsGrades: number[] = [];
  userPropertiesReviewsGrades: number[] = [];

  condition: number = 1;

  page: number = 1;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    public authService: AuthorizationService,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  getUsersReviews(value: boolean): void {
    this.condition = 1;

    if (value) {
      this.page = 1;
      this.usersReviews = [];
      this.userReviewsGrades = [];
    }

    fetch(
      `https://apartmain.azurewebsites.net/api/reviews/getusersreviews?email=${AuthHelper.getLogin()}&page=${
        this.page
      }`,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.userCountryImage = response.reviews[0].countryImage.name;
          this.userFirstName = response.reviews[0].firstName;
          this.userLastName = response.reviews[0].lastName;

          for (let i = 0; i < response.reviews.length; i++) {
            this.usersReviews.push(response.reviews[i]);
          }
          for (let i = 0; i < response.reviewGrades.length; i++) {
            this.userReviewsGrades.push(response.reviewGrades[i]);
          }
          this.page++;
        } else {
          this.showAlert(response.message);
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  getUserPropertiesReviews(value: boolean): void {
    this.condition = 2;

    if (value) {
      this.page = 1;
      this.usersPropertiesReviews = [];
      this.userPropertiesReviewsGrades = [];
    }

    fetch(
      `https://apartmain.azurewebsites.net/api/reviews/getuserpropertiesreviews?email=${AuthHelper.getLogin()}&page=${
        this.page
      }`,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.userCountryImage = response.reviews[0].countryImage.name;
          this.userFirstName = response.reviews[0].firstName;
          this.userLastName = response.reviews[0].lastName;

          for (let i = 0; i < response.reviews.length; i++) {
            this.usersPropertiesReviews.push(response.reviews[i]);
          }
          for (let i = 0; i < response.reviewGrades.length; i++) {
            this.userPropertiesReviewsGrades.push(response.reviewGrades[i]);
          }
          this.page++;
        } else {
          this.showAlert(response.message);
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  ngOnInit(): void {
    if (!AuthHelper.isLogged()) {
      this.router.navigate(['']);
    }

    this.getUsersReviews(false);
  }
}
