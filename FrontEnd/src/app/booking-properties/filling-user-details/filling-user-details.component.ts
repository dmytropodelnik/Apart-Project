import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

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
    isSuite: boolean;
    isSmokingAllowed: boolean;
    facilities: any;
  }[] = [];

  constructor(
    private router: Router,
    private activatedRouter: ActivatedRoute,) { }

  getParams(): void {
    this.activatedRouter.queryParams.subscribe((params: any) => {

      if (params['chosenApartments']) {
        for (let i = 0; i < params['chosenApartments'].length; i++) {
          if (params['chosenApartments'][i].amount > 0) {
            this.chosenApartments.push(params['chosenApartments'][i]);
          }
        }
      }
    });
  }

  ngOnInit(): void {

  }

}
