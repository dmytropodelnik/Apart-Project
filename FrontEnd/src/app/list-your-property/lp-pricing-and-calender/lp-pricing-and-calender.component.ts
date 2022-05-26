import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { ListNewPropertyService } from '../../services/list-new-property.service';

import AuthHelper from '../../utils/authHelper';

@Component({
  selector: 'app-lp-pricing-and-calender',
  templateUrl: './lp-pricing-and-calender.component.html',
  styleUrls: ['./lp-pricing-and-calender.component.css']
})
export class LpPricingAndCalenderComponent implements OnInit {
  savedPropertyId: string = '';
  price: string = '';

  constructor(
    private listNewPropertyService: ListNewPropertyService,
    private router: Router,
  ) {

  }

  addPropertyPrice(): void {
    let suggestion = {
      id: this.listNewPropertyService.getSavedPropertyId(),
      priceInUs: this.price,
      priceInUserCurrency: this.price,
    };

    fetch(`https://apartmain.azurewebsites.net/api/listnewproperty/addprice`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(suggestion),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.router.navigate(['/lp/reviewandcomplete']);
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  ngOnInit(): void {
    if (!AuthHelper.isLogged()) {
      this.router.navigate(['']);
    }
    else if (!this.listNewPropertyService.getSavedPropertyId()) {
      this.router.navigate(['']);
    }
  }

}
