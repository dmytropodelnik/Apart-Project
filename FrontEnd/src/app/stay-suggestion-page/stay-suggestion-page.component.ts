import { Component, OnInit } from '@angular/core';

import AuthHelper from '../utils/authHelper';
import ImageHelper from '../utils/imageHelper';
import MathHelper from '../utils/mathHelper';
import { Router } from '@angular/router';
import { ViewportScroller } from '@angular/common';
import { ExtraOptions } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { NgbActiveModal, NgbDate } from '@ng-bootstrap/ng-bootstrap';
import { switchMap } from 'rxjs/operators';
import { FilterViewModel } from '../view-models/filterviewmodel.item';
import { SearchViewModel } from '../view-models/searchviewmodel.item';
import { Suggestion } from '../models/Suggestions/suggestion.item';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FacilityType } from '../models/facilitytype.item';
import { SuggestionRuleType } from '../models/Suggestions/suggestionruletype.item';
import { Review } from '../models/Review/review.item';
import { ReviewCategory } from '../models/Review/reviewcategory.item';

@Component({
  selector: 'app-stay-suggestion-page',
  templateUrl: './stay-suggestion-page.component.html',
  styleUrls: ['./stay-suggestion-page.component.css'],
})
export class StaySuggestionPageComponent implements OnInit {
  uniqueCode: number | undefined;

  mathHelper: any = MathHelper;
  imageHelper: any = ImageHelper;
  authHelper: any = AuthHelper;

  filters: SearchViewModel = new SearchViewModel();
  filterChecks: FilterViewModel[] = [];

  suggestion: any;
  facilityTypes: FacilityType[] = [];
  ruleTypes: SuggestionRuleType[] = [];

  searchBookingCategory: string = '';
  searchPlace: string = '';

  yearIn: number | null = null;
  monthIn: number | null = null;
  dayIn: number | null = null;

  yearOut: number | null = null;
  monthOut: number | null = null;
  dayOut: number | null = null;

  reviewsAmount: number = 0;
  reviewsPage: number = 0;
  reviews: any[] = [];
  reviewGrades: number[] = [];
  reviewCategories: ReviewCategory[] = [];
  categoryGrades: number[] = [];

  grade: number = 0;

  isSaved: string = 'false';

  isAuth: boolean = false;

  constructor(
    private router: Router,
    private activatedRouter: ActivatedRoute,
    private modalService: NgbModal,
    private scroller: ViewportScroller
  ) {}

  routerOptions: ExtraOptions = {
    anchorScrolling: 'enabled',
    //scrollPositionRestoration: "enabled"
  };
  openWindowCustomClass(longContent: any) {
    this.getSuggestionReviews();
    this.modalService.open(longContent, {
      modalDialogClass: 'modal-fullscreen',
    });
  }

  openVerticallyCentered(content: any) {
    this.modalService.open(content, {
      centered: true,
    });
  }

  goDown1() {
    this.scroller.scrollToAnchor('targetAvailability');
  }
  goDown2() {
    this.scroller.scrollToAnchor('targetFacilities');
  }

  goDown3() {
    this.scroller.scrollToAnchor('targetHouserules');
  }

  getSuggestion(): void {
    fetch(
      'https://localhost:44381/api/suggestions/getsuggestion?code=' +
        this.uniqueCode,
      {
        method: 'GET',
      }
    )
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.suggestion = response.suggestion;
          this.suggestion.reviewsAmount = response.reviewsAmount;
          this.facilityTypes = response.facilities;
          this.ruleTypes = response.rules;
          this.reviewsAmount = response.reviewsAmount;
          this.grade = response.grade;
          //alert(this.suggestion.id);
          console.log(this.suggestion);
        } else {
          alert('Suggestion fetching error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  loadMoreReviews(): void {
    this.reviewsPage++;
    fetch(
      `https://localhost:44381/api/reviews/getsuggestionreviews?id=${this.suggestion.id}&page=${this.reviewsPage}`,
      {
        method: 'GET',
      }
    )
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          for (let i = 0; i < response.reviews.length; i++) {
            this.reviews.push(response.reviews[i]);
          }
        } else {
          alert('Suggestion reviews fetching error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addSuggestionToSaved(id: any): void {
    const suggestion = {
      id: id,
      login: AuthHelper.getLogin(),
    };

    fetch('https://localhost:44381/api/favorites/addsuggestion', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(suggestion),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.isSaved = 'true';
        } else {
          alert('User favorites fetching error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  removeSuggestion(id: any): void {
    const suggestion = {
      id: id,
      login: AuthHelper.getLogin(),
    };

    fetch('https://localhost:44381/api/favorites/removesuggestion', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(suggestion),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.isSaved = 'false';
        } else {
          alert('User favorites fetching error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  addMainSearchFilter(): void {
    this.activatedRouter.queryParams.subscribe((params: any) => {
      this.filters.place = params['place'];
      this.filters.dateIn = params['dateIn'];
      this.filters.dateOut = params['dateOut'];
      this.filters.pdateIn = new NgbDate(
        +params['yearIn'],
        +params['monthIn'],
        +params['dayIn']
      );
      this.filters.pdateOut = new NgbDate(
        +params['yearOut'],
        +params['monthOut'],
        +params['dayOut']
      );

      this.yearIn = +params['yearIn'];
      this.monthIn = +params['monthIn'];
      this.dayIn = +params['dayIn'];

      this.yearOut = +params['yearOut'];
      this.monthOut = +params['monthOut'];
      this.dayOut = +params['dayOut'];

      this.filters.searchAdultsAmount = params['adults'];
      this.filters.searchChildrenAmount = params['children'];
      this.filters.searchRoomsAmount = params['rooms'];

      this.searchBookingCategory = params['bookingCategory'];

      this.isSaved = params['isSaved'];

      if (this.filters.place) {
        this.filterChecks.push(
          new FilterViewModel('places', this.filters.place)
        );
        this.searchPlace = this.filters.place;
      }
      if (
        typeof this.filters.dateIn !== 'undefined' ||
        typeof this.filters.dateOut !== 'undefined'
      ) {
        this.filterChecks.push(
          new FilterViewModel(
            'dates',
            this.filters.dateIn + ';' + this.filters.dateOut
          )
        );
      }
      this.filterChecks.push(
        new FilterViewModel(
          'amounts',
          this.filters.searchAdultsAmount +
            ';' +
            this.filters.searchChildrenAmount +
            ';' +
            this.filters.searchRoomsAmount
        )
      );
      if (this.searchBookingCategory) {
        this.filterChecks.push(
          new FilterViewModel('bookingCategories', this.searchBookingCategory)
        );
        this.searchPlace = this.searchBookingCategory;
      }
    });
  }

  likeComment(id: number): void {
    fetch(
      `https://localhost:44381/api/reviews/likereview?id=${id}&email=${AuthHelper.getLogin()}`,
      {
        method: 'PUT',
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
          this.reviews[id - 1].likes = data.reviewData.likes;
          this.reviews[id - 1].dislikes = data.reviewData.dislikes;
          alert('Liked successfully!');
        } else {
          alert(data.message);
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  dislikeComment(id: number): void {
    fetch(
      `https://localhost:44381/api/reviews/dislikereview?id=${id}&email=${AuthHelper.getLogin()}`,
      {
        method: 'PUT',
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
          this.reviews[id - 1].likes = data.reviewData.likes;
          this.reviews[id - 1].dislikes = data.reviewData.dislikes;
          alert('Disliked successfully!');
        } else {
          alert(data.message);
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getSuggestionReviews(): void {
    this.reviewsPage = 1;
    fetch(
      `https://localhost:44381/api/reviews/getsuggestionreviews?id=${this.suggestion.id}&page=${this.reviewsPage}`,
      {
        method: 'GET',
      }
    )
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.reviews = response.reviews;
          this.reviewGrades = response.reviewGrades;
          this.reviewCategories = response.reviewCategories;
          this.categoryGrades = response.categoryGrades;
        } else {
          alert('Suggestion reviews fetching error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  ngOnInit(): void {
    this.activatedRouter.paramMap
      .pipe(switchMap((params) => params.getAll('id')))
      .subscribe((data) => (this.uniqueCode = +data));

    this.addMainSearchFilter();
    this.getSuggestion();
  }
}
