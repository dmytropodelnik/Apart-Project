import { Component, OnInit } from '@angular/core';
import { DateModel } from 'src/app/models/datemodel.item';
import { PromoCode } from 'src/app/models/Payment/promocode.item';

import AuthHelper from '../../utils/authHelper';

@Component({
  selector: 'app-promocode-generator',
  templateUrl: './promocode-generator.component.html',
  styleUrls: ['./promocode-generator.component.css'],
})
export class PromocodeGeneratorComponent implements OnInit {
  promoCodes: PromoCode[] = [];
  count: number | null = null;
  discount: number = 0;
  dp: DateModel = new DateModel();

  displayMonths = 2;
  navigation = 'arrows';
  showWeekNumbers = false;
  outsideDays = 'hidden';

  constructor() {}

  generatePromoCode(): void {
    let expirationDate = this.dp.year + '-' + this.dp.month + '-' + this.dp.day;
    console.log(expirationDate);

    fetch(
      `https://localhost:44381/api/promocodes/generatecode?discount=${this.discount}&count=${this.count}&expirationDate=${expirationDate}`,
      {
        method: 'POST',
        headers: {
          Accept: 'application/json',
          Authorization: 'Bearer ' + AuthHelper.getToken(),
        },
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          fetch(`https://localhost:44381/api/promocodes/getlastcodes`, {
            method: 'GET',
            headers: {
              Accept: 'application/json',
              Authorization: 'Bearer ' + AuthHelper.getToken(),
            },
          })
            .then((r) => r.json())
            .then((data) => {
              if (data.code === 200) {
                this.promoCodes = data.codes;
              } else {
                alert('Fetching last promo codes error!');
              }
            })
            .catch((ex) => {
              alert(ex);
            });
        } else {
          alert('Generating promo codes error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  ngOnInit(): void {
    fetch(`https://localhost:44381/api/promocodes/getlastcodes`, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.promoCodes = data.codes;
        } else {
          alert('Fetching last promo codes error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }
}