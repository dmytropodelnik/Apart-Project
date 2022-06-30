import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MainDataService } from 'src/app/services/main-data.service';

import { ListNewPropertyService } from '../../services/list-new-property.service';

import AuthHelper from '../../utils/authHelper';

@Component({
  selector: 'app-lp-pricing-and-calender',
  templateUrl: './lp-pricing-and-calender.component.html',
  styleUrls: ['./lp-pricing-and-calender.component.css'],
})
export class LpPricingAndCalenderComponent implements OnInit {
  savedPropertyId: string = '';
  price: string = '';

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private listNewPropertyService: ListNewPropertyService,
    private router: Router,
    private activatedRouter: ActivatedRoute,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  addPropertyPrice(): void {
    let suggestion = {
      id: this.listNewPropertyService.getSavedPropertyId(),
      priceInUs: this.price,
      priceInUserCurrency: this.price,
    };

    fetch(`https://localhost:44381/api/listnewproperty/addprice`, {
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
        this.showAlert(ex);
      });
  }

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  ngOnInit(): void {
    this.activatedRouter.queryParams.subscribe((params: any) => {
      if (params['toSaveId'] == 'true') {
        this.listNewPropertyService.setSavedPropertyId(params['id']);
      }
      if (!AuthHelper.isLogged()) {
        this.router.navigate(['']);
      } else if (!this.listNewPropertyService.getSavedPropertyId()) {
        this.router.navigate(['']);
      }
    });
  }
}
