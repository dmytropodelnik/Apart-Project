import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';

import AuthHelper from '../utils/authHelper';
import BookingHelper from '../utils/bookingHelper';
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
import { BookingDetailsService } from '../services/booking-details.service';
import { MainDataService } from '../services/main-data.service';

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

  booking: any;

  chosenApartments: {
    id: number;
    name: string;
    amount: number;
    roomsAmount: number;
    guestsLimit: number;
    bathroomsAmount: number;
    apartmentSize: number;
    priceInUSD: number;
    isSuite: string;
    isSmokingAllowed: string;
  }[] = [];

  chosenFinalApartments: {
    name: string;
    amount: number;
    roomsAmount: number;
    guestsLimit: number;
    bathroomsAmount: number;
    apartmentSize: number;
    priceInUSD: number;
    isSuite: string;
    isSmokingAllowed: string;
  }[] = [];

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

  isDateChosen: boolean = false;
  isOwnerVerified: boolean = true;

  diffDays: number = 0;

  bookingNumber: string = '';
  bookingPin: string = '';

  reviewCategoryGrades: number[] = [-1, -1, -1, -1, -1, -1];

  title: string = '';
  positiveSide: string = '';
  negativeSide: string = '';

  owner: string = '';

  page: number = 1;
  current = new Date();
  minDate = {
    year: this.current.getFullYear(),
    month: this.current.getMonth() + 1,
    day: this.current.getDate(),
  };

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private router: Router,
    private activatedRouter: ActivatedRoute,
    private scroller: ViewportScroller,
    private bookingDetailsService: BookingDetailsService,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  routerOptions: ExtraOptions = {
    anchorScrolling: 'enabled',
    //scrollPositionRestoration: "enabled"
  };

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

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

  fillApartmentsArray(): void {
    for (let i = 0; i < this.suggestion.apartments.length; i++) {
      this.chosenApartments.push({
        id: this.suggestion.apartments[i].id,
        name: this.suggestion.apartments[i].name,
        amount: -1,
        roomsAmount: this.suggestion.apartments[i].roomsAmount,
        guestsLimit: this.suggestion.apartments[i].guestsLimit,
        bathroomsAmount: this.suggestion.apartments[i].bathroomsAmount,
        apartmentSize: this.suggestion.apartments[i].apartmentSize,
        priceInUSD: this.suggestion.apartments[i].priceInUSD,
        isSuite: this.suggestion.apartments[i].isSuite,
        isSmokingAllowed: this.suggestion.apartments[i].isSmokingAllowed,
      });
    }
  }

  resetReviewsData(): void {
    this.page = 1;
  }

  resetData(): void {
    this.isOwnerVerified = false;
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
          this.fillApartmentsArray();
        } else {
          this.showAlert('Suggestion fetching error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
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
          this.page++;
        } else {
          this.showAlert('Suggestion reviews fetching error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  addSuggestionToSaved(id: any): void {
    if (!AuthHelper.isLogged()) {
      this.router.navigate(['auth']);
      return;
    }

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
          this.showAlert('User favorites fetching error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  removeSuggestion(id: any): void {
    if (!AuthHelper.isLogged()) {
      this.router.navigate(['auth']);
      return;
    }

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
          this.showAlert('User favorites fetching error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  chooseApartments(): void {
    if (!this.isDateChosen) {
      this.showAlert('Select the check in and check out dates!');
      return;
    }

    for (let i = 0; i < this.chosenApartments.length; i++) {
      if (this.chosenApartments[i].amount > 0) {
        for (let j = 0; j < this.chosenApartments[i].amount; j++) {
          this.chosenFinalApartments.push(this.suggestion.apartments[i]);
        }
      }
    }
    if (this.chosenFinalApartments.length == 0) {
      this.showAlert('Select apartments amount!');
      return;
    }

    let dateIn = new Date(`${this.monthIn}/${this.dayIn}/${this.yearIn}`);
    let dateOut = new Date(`${this.monthOut}/${this.dayOut}/${this.yearOut}`);

    let diffDate = (dateOut as any) - (dateIn as any);

    let milliseconds = diffDate;
    let seconds = milliseconds / 1000;
    let minutes = seconds / 60;
    let hours = minutes / 60;

    this.diffDays = hours / 24;
    this.bookingDetailsService.setChosenDates(
      dateIn.toDateString(),
      dateOut.toDateString()
    );
    this.bookingDetailsService.setChosenApartmentsAndSuggestion(
      this.chosenFinalApartments,
      this.suggestion,
      this.grade,
      this.diffDays
    );
    BookingHelper.saveBookingData(
      this.suggestion,
      this.chosenFinalApartments,
      this.grade,
      this.diffDays,
      dateIn,
      dateOut
    );
    this.router.navigate(['/fillinguserdetails']);
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

      if (
        this.filters.pdateIn?.day != null &&
        this.filters.pdateOut?.day != null &&
        this.filters.pdateIn?.month != null &&
        this.filters.pdateOut?.month != null &&
        this.filters.pdateIn?.year != null &&
        this.filters.pdateOut?.year != null
      ) {
        this.isDateChosen = true;
      }

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

  likeComment(id: number, index: number): void {
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
          this.reviews[index].likes = data.reviewData.likes;
          this.reviews[index].dislikes = data.reviewData.dislikes;
          this.showAlert('Liked successfully!');
        } else {
          this.showAlert(data.message);
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  dislikeComment(id: number, index: number): void {
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
          this.reviews[index].likes = data.reviewData.likes;
          this.reviews[index].dislikes = data.reviewData.dislikes;
          this.showAlert('Disliked successfully!');
        } else {
          this.showAlert(data.message);
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
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
          this.showAlert('Suggestion reviews fetching error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  filterApartments(): void {
    let dateIn, dateOut;

    if (
      this.filters.pdateIn?.day != null &&
      this.filters.pdateOut?.day != null &&
      this.filters.pdateIn?.month != null &&
      this.filters.pdateOut?.month != null &&
      this.filters.pdateIn?.year != null &&
      this.filters.pdateOut?.year != null
    ) {
      dateIn =
        this.filters.pdateIn!.year +
        '-' +
        this.filters.pdateIn!.month +
        '-' +
        this.filters.pdateIn!.day;
      dateOut =
        this.filters.pdateOut!.year +
        '-' +
        this.filters.pdateOut!.month +
        '-' +
        this.filters.pdateOut!.day;

      this.dayIn = this.filters.pdateIn!.day;
      this.monthIn = this.filters.pdateIn!.month;
      this.yearIn = this.filters.pdateIn!.year;
      this.dayOut = this.filters.pdateOut!.day;
      this.monthOut = this.filters.pdateOut!.month;
      this.yearOut = this.filters.pdateOut!.year;
    } else {
      this.showAlert('Select the check in and check out date!');
      return;
    }

    const filters = {
      dateIn: dateIn,
      dateOut: dateOut,
      searchRoomsAmount: this.filters.searchRoomsAmount,
      guestsAmount:
        +this.filters.searchAdultsAmount + +this.filters.searchChildrenAmount,
      suggestionId: this.suggestion.id,
    };

    fetch(`https://localhost:44381/api/apartments/filter`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(filters),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.suggestion.apartments = response.apartments;
          this.isDateChosen = true;
        } else {
          this.showAlert(response.message + '123');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  rateStay(): void {
    if (this.bookingNumber.length != 8) {
      this.showAlert('Enter a correct booking number!');
      return;
    }
    if (this.bookingPin.length < 6 && this.bookingPin.length > 8) {
      this.showAlert('Enter a correct booking PIN!');
      return;
    }

    fetch(
      `https://localhost:44381/api/staybookings/verifyowner?bookingNumber=${this.bookingNumber}&bookingPIN=${this.bookingPin}`,
      {
        method: 'GET',
      }
    )
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.isOwnerVerified = true;
          this.owner = response.owner;
          this.booking = response.booking;
        } else {
          this.showAlert(response.message);
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  submitReview(): void {
    if (this.title.length < 6) {
      this.showAlert('Review title must contain at least 6 characters!');
      return;
    }
    if (this.positiveSide.length < 10) {
      this.showAlert(
        'Positive side of review must contain at least 10 characters!'
      );
      return;
    }
    if (this.negativeSide.length < 10) {
      this.showAlert(
        'Negative side of review must contain at least 10 characters!'
      );
      return;
    }

    for (let i = 0; i < this.reviewCategoryGrades.length; i++) {
      if (this.reviewCategoryGrades[i] == -1) {
        this.showAlert('Select review grade for each category!');
        return;
      }
    }

    const review = {
      bookingNumber: this.bookingNumber,
      bookingPIN: this.bookingPin,
      suggestionId: this.suggestion.id,
      owner: this.owner,
      ownerFirstName: this.booking.customerInfo.firstName,
      ownerLastName: this.booking.customerInfo.lastName,
      addressText: this.booking.customerInfo.addressText,
      city: this.booking.customerInfo.city,
      country: this.booking.customerInfo.country,
      ownerPhoneNumber: this.booking.customerInfo.phoneNumber,
      reviewMessage: {
        title: this.title,
        positiveText: this.positiveSide,
        negativeText: this.negativeSide,
      },
      grades: [
        {
          grade: this.reviewCategoryGrades[0],
          reviewCategoryId: 1,
        },
        {
          grade: this.reviewCategoryGrades[1],
          reviewCategoryId: 2,
        },
        {
          grade: this.reviewCategoryGrades[2],
          reviewCategoryId: 3,
        },
        {
          grade: this.reviewCategoryGrades[3],
          reviewCategoryId: 4,
        },
        {
          grade: this.reviewCategoryGrades[4],
          reviewCategoryId: 5,
        },
        {
          grade: this.reviewCategoryGrades[5],
          reviewCategoryId: 6,
        },
      ],
    };

    fetch(`https://localhost:44381/api/reviews/addreview`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(review),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.getSuggestionReviews();
          this.reviewsAmount++;
          this.isOwnerVerified = false;
        } else {
          this.showAlert(response.message);
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
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
