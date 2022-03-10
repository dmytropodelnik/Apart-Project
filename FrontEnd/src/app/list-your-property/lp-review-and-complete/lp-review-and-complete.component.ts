import { Component, OnInit } from '@angular/core';

import { ListNewPropertyService } from '../../services/list-new-property.service';

import AuthHelper from '../../utils/authHelper';

@Component({
  selector: 'app-lp-review-and-complete',
  templateUrl: './lp-review-and-complete.component.html',
  styleUrls: ['./lp-review-and-complete.component.css']
})
export class LpReviewAndCompleteComponent implements OnInit {

  constructor(
    private listNewPropertyService: ListNewPropertyService,
  ) {}

  ngOnInit(): void {
    
  }

}
