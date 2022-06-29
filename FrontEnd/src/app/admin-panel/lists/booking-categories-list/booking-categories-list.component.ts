import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BookingCategory } from 'src/app/models/bookingcategory.item';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-booking-categories-list',
  templateUrl: './booking-categories-list.component.html',
  styleUrls: ['./booking-categories-list.component.css'],
})
export class BookingCategoriesListComponent implements OnInit {
  categories: BookingCategory[] | null = null;
  category: BookingCategory;
  searchCategory: string = '';
  checkedCategory: number | null = null;

  page: number = 1;
  pageSize: number = 15;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private adminContentService: AdminContentService,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {
    this.category = new BookingCategory();
  }

  search(): void {
    fetch(
      'https://localhost:44381/api/bookingcategories/search?category=' +
        this.searchCategory,
      {
        method: 'GET',
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
          this.categories = data.categories;
        } else {
          alert('Search error!');
        }
        this.searchCategory = '';
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  addCategory(): void {
    let category = {
      category: this.category.category,
    };

    fetch('https://localhost:44381/api/bookingcategories/addcategory', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(category),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCategories();
        } else {
          alert('Adding error!');
        }
        this.category.category = '';
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  editCategory(): void {
    let category = {
      id: this.checkedCategory,
      category: this.category.category,
    };

    fetch('https://localhost:44381/api/bookingcategories/editcategory', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(category),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCategories();
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.category.category = '';
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  deleteCategory(): void {
    let category = {
      id: this.checkedCategory,
      category: this.category.category,
    };

    fetch('https://localhost:44381/api/bookingcategories/deletecategory', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(category),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCategories();
          ListHelper.disableButtons();
        } else {
          alert('Deleting error!');
        }
        this.category.category = '';
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  getCategories(): void {
    fetch(
      `https://localhost:44381/api/bookingcategories/getcategories?page=${this.page}&pageSize=${this.pageSize}`,
      {
        method: 'GET',
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
          this.categories = data.categories;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  collectElements(categories: BookingCategory[]): void {
    for (let item of categories) {
      this.categories?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(
      `https://localhost:44381/api/bookingcategories/getcategories?page=${this.page}&pageSize=${this.pageSize}`,
      {
        method: 'GET',
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
          this.collectElements(data.categories);
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  setCategory(category: BookingCategory): void {
    this.checkedCategory = category.id;
    this.category.category = category.category;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getCategories();
  }
}
