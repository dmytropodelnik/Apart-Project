import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MainDataService } from 'src/app/services/main-data.service';

import { ListNewPropertyService } from '../../services/list-new-property.service';

import AuthHelper from '../../utils/authHelper';

@Component({
  selector: 'app-add-property',
  templateUrl: './add-property.component.html',
  styleUrls: ['./add-property.component.css'],
})
export class AddPropertyComponent implements OnInit {
  bookingCategoryId: number = -1;
  choice: number = 0;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private listNewPropertyService: ListNewPropertyService,
    private router: Router,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  addApartment(): void {
    this.choice = 1;
    this.getBookingCategories();
  }

  addHome(): void {
    this.choice = 2;
    this.getBookingCategories();
  }

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  addHotel(): void {
    this.choice = 3;
    this.getBookingCategories();
  }

  addAlternativePlace(): void {
    this.choice = 4;
    this.getBookingCategories();
  }

  addBookingCategory(): void {
    let suggestion = {
      bookingCategoryId: this.bookingCategoryId,
      login: AuthHelper.getLogin(),
    };

    fetch(`https://localhost:44381/api/listnewproperty/addbookingcategory`, {
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
          this.listNewPropertyService.setSavedPropertyId(
            data.savedSuggestionId
          );
          this.router.navigate(['lp/nameandlocation']);
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  getBookingCategories(): void {
    fetch(
      'https://localhost:44381/api/bookingcategories/getcategoriesforlist?categoryTypeId=' +
        this.choice,
      {
        method: 'GET',
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          let categoryAdd = document.getElementById('categoriesSelect');
          let newOption;
          for (let item of data.categories) {
            newOption = new Option(item.category, item.id);
            categoryAdd?.append(newOption);
          }
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  ngOnInit(): void {
    if (!AuthHelper.isLogged()) {
      this.router.navigate(['']);
    }
  }
}
