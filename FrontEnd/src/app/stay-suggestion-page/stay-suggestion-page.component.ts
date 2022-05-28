import { Component, OnInit } from '@angular/core';

import AuthHelper from '../utils/authHelper';
import ImageHelper from '../utils/imageHelper';
import MathHelper from '../utils/mathHelper';

import { ActivatedRoute } from '@angular/router';
import { NgbDate } from '@ng-bootstrap/ng-bootstrap';
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
  reviews: any;
  reviewGrades: number[] = [];
  reviewCategories: ReviewCategory[] = [];
  categoryGrades: number[] = [];

  grade: number = 0;

  constructor(
    private activatedRouter: ActivatedRoute,
    private modalService: NgbModal
  ) {}

  openWindowCustomClass(longContent: any) {
    this.getSuggestionReviews();
    this.modalService.open(longContent, {
      modalDialogClass: 'modal-fullscreen',
    });
  }

  openVerticallyCentered(content : any) {
    this.modalService.open(content, { centered: true });
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
          this.ruleTypes = response.rules
          this.reviewsAmount = response.reviewsAmount;
          this.grade = response.grade;
          //alert(this.suggestion.id);
        } else {
          alert('Suggestion fetching error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  loadMoreReviews(): void {
    this.getSuggestionReviews();
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

  getSuggestionReviews(): void {
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
          this.reviews += response.reviews;
          this.reviewCategories = response.reviewCategories;
          this.categoryGrades = response.categoryGrades;
          console.log(this.suggestion.reviews);
          console.log(this.reviewCategories);
          console.log(this.categoryGrades);
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
