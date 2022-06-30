import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ContactDetails } from 'src/app/models/Suggestions/contactdetails.item';
import { MainDataService } from 'src/app/services/main-data.service';

import { ListNewPropertyService } from '../../services/list-new-property.service';

import AuthHelper from '../../utils/authHelper';

@Component({
  selector: 'app-lp-review-and-complete',
  templateUrl: './lp-review-and-complete.component.html',
  styleUrls: ['./lp-review-and-complete.component.css'],
})
export class LpReviewAndCompleteComponent implements OnInit {
  contactDetails: ContactDetails = new ContactDetails();

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private listNewPropertyService: ListNewPropertyService,
    private router: Router,
    private activatedRouter: ActivatedRoute,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  addContactDetails(): void {
    let suggestion = {
      id: this.listNewPropertyService.getSavedPropertyId(),
      contactFirstName: this.contactDetails?.firstName,
      contactLastName: this.contactDetails?.lastName,
      contactPhone: this.contactDetails?.phoneNumber,
      contactEmail: this.contactDetails?.email,
    };

    fetch(`https://localhost:44381/api/listnewproperty/addcontactdetails`, {
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
          this.listNewPropertyService.setSavedPropertyId('');
          this.router.navigate(['/viewproperties']);
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
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
