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
      contactName: this.contactDetails?.contactName,
      contactPhone: this.contactDetails?.phoneNumber,
    };
    console.log(suggestion);

    fetch(`http://apartmain.azurewebsites.net/api/listnewproperty/addcontactdetails`, {
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
          this.router.navigate(['/joinpartner']);
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  ngOnInit(): void {

  }

}
