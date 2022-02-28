import { Component, OnInit } from '@angular/core';

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
  ) {

  }

  addPropertyPrice(): void {
    let suggestion = {
      id: 1, // this.savedPropertyId,
      priceInUs: 2,  //
      priceInUserCurrency: 2,  //
    };

    fetch(`https://localhost:44381/api/listnewproperty/addrules`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(suggestion),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
        }
        console.log(data);
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  ngOnInit(): void {
  }

}
