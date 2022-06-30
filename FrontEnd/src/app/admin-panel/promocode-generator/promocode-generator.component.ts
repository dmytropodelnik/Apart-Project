import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DateModel } from 'src/app/models/datemodel.item';
import { PromoCode } from 'src/app/models/Payment/promocode.item';
import { MainDataService } from 'src/app/services/main-data.service';

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

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  generatePromoCode(): void {
    let expirationDate = this.dp.year + '-' + this.dp.month + '-' + this.dp.day;

    fetch(
      `https://localhost:44381/api/promocodes/generatecode?discount=${this.discount}&count=${this.count}&expirationDate=${expirationDate}`,
      {
        method: 'POST',
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
          fetch(`https://localhost:44381/api/promocodes/getlastcodes`, {
            method: 'GET',
            headers: {
              'Content-Type': 'application/json; charset=utf-8',
              Accept: 'application/json',
              Authorization:
                AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
            },
          })
            .then((r) => r.json())
            .then((data) => {
              if (data.code === 200) {
                this.promoCodes = data.codes;
              } else {
                this.showAlert('Fetching last promo codes error!');
              }
            })
            .catch((ex) => {
              this.mainDataService.alertContent = ex;
              this.modalService.open(this.alert);
            });
        } else {
          this.showAlert('Generating promo codes error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  ngOnInit(): void {
    fetch(`https://localhost:44381/api/promocodes/getlastcodes`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.promoCodes = data.codes;
        } else {
          this.showAlert('Fetching last promo codes error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }
}
