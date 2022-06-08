import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

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
    isSmokingAllowed: string;
  }[] = [];

  constructor(
    private router: Router,
    private activatedRouter: ActivatedRoute,) { }

  getParams(): void {
    this.activatedRouter.queryParams.subscribe((params: any) => {
      console.log(params['chosenApartments']);
      if (params['chosenApartments'].length > 0) {
        for (let i = 0; i < params['chosenApartments'].length; i++) {
          if (params['chosenApartments'][i].amount > 0) {
            this.chosenApartments.push(params['chosenApartments'][i]);
          }
          console.log(this.chosenApartments[i]);
        }
      } else {
        this.router.navigate(['']);
      }

    });
  }

  ngOnInit(): void {
    this.getParams();
  }

}
