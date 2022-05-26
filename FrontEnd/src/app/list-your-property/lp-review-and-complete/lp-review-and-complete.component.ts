import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ContactDetails } from 'src/app/models/Suggestions/contactdetails.item';

import { ListNewPropertyService } from '../../services/list-new-property.service';

import AuthHelper from '../../utils/authHelper';

@Component({
  selector: 'app-lp-review-and-complete',
  templateUrl: './lp-review-and-complete.component.html',
  styleUrls: ['./lp-review-and-complete.component.css']
})
export class LpReviewAndCompleteComponent implements OnInit {
  contactDetails: ContactDetails = new ContactDetails();

  constructor(
    private listNewPropertyService: ListNewPropertyService,
    private router: Router,
  ) {

  }

  addContactDetails(): void {
    let suggestion = {
      id: this.listNewPropertyService.getSavedPropertyId(),
      contactFirstName: this.contactDetails?.firstName,
      contactLastName: this.contactDetails?.lastName,
      contactPhone: this.contactDetails?.phoneNumber,
      contactEmail: this.contactDetails?.email,
    };

    fetch(`https://apartmain.azurewebsites.net/api/listnewproperty/addcontactdetails`, {
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
