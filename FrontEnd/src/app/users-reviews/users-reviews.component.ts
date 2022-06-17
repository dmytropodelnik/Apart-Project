import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import AuthHelper from '../utils/authHelper';
import ImageHelper from '../utils/imageHelper';
import MathHelper from '../utils/mathHelper';

@Component({
  selector: 'app-users-reviews',
  templateUrl: './users-reviews.component.html',
  styleUrls: ['./users-reviews.component.css'],
})
export class UsersReviewsComponent implements OnInit {
  bookings: any[] = [];

  imageHelper: any = ImageHelper;

  selectedBooking: number = 0;

  constructor(
    private modalService: NgbModal,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {}

  

  ngOnInit(): void {
    if (!AuthHelper.isLogged()) {
      this.router.navigate(['']);
    }
  }
}
