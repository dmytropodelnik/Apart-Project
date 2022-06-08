import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SuggestionDetailsService } from 'src/app/services/suggestion-details.service';

import AuthHelper from '../../utils/authHelper';
import ImageHelper from '../../utils/imageHelper';
import MathHelper from '../../utils/mathHelper';

@Component({
  selector: 'app-filling-user-details',
  templateUrl: './filling-user-details.component.html',
  styleUrls: ['./filling-user-details.component.css']
})
export class FillingUserDetailsComponent implements OnInit {

  chosenApartments: {
    name: string;
    amount: number;
    roomsAmount: number;
    guestsLimit: number;
    bathroomsAmount: number;
    apartmentSize: number;
    isSuite: string;
    isSmokingAllowed: boolean;
  }[] = [];

  constructor(
    private router: Router,
    private activatedRouter: ActivatedRoute,
    private suggestionDetailsService: SuggestionDetailsService,
    ) {

    }

  ngOnInit(): void {
    if (this.suggestionDetailsService.getChosenApartments() != null) {
      this.chosenApartments = this.suggestionDetailsService.getChosenApartments();
      console.log(this.chosenApartments);
    } else {
      console.log(this.chosenApartments);
      this.router.navigate(['']);
    }
  }

}
