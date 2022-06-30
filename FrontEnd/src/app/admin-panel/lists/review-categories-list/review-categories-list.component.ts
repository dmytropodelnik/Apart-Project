import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ReviewCategory } from 'src/app/models/Review/reviewcategory.item';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-review-categories-list',
  templateUrl: './review-categories-list.component.html',
  styleUrls: ['./review-categories-list.component.css'],
})
export class ReviewCategoriesListComponent implements OnInit {
  categories: ReviewCategory[] | null = null;
  category: ReviewCategory | null = null;
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
  ) {}

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  search(): void {
    fetch(
      'https://apartmain.azurewebsites.net/api/reviewcategories/search?category=' +
        this.searchCategory,
      {
        method: 'GET',
        headers: {
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
          this.showAlert('Search error!');
        }
        this.searchCategory = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  addCategory(): void {
    let category = {
      name: this.category,
    };

    fetch('https://apartmain.azurewebsites.net/api/categories/addcategory', {
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
          this.showAlert('Adding error!');
        }
        this.category = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  editCategory(): void {
    let category = {
      id: this.checkedCategory,
      name: this.category,
    };

    fetch('https://apartmain.azurewebsites.net/api/categories/editcategory', {
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
          this.showAlert('Editing error!');
        }
        this.category = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  deleteCategory(): void {
    let category = {
      id: this.checkedCategory,
      name: this.category,
    };

    fetch('https://apartmain.azurewebsites.net/api/categories/deletecategory', {
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
          this.showAlert('Editing error!');
        }
        this.category = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  getCategories(): void {
    fetch(
      `https://apartmain.azurewebsites.net/api/reviewcategories/getcategories?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  collectElements(categories: ReviewCategory[]): void {
    for (let item of categories) {
      this.categories?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(
      `https://apartmain.azurewebsites.net/api/reviewcategories/getcategories?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  setCategory(category: ReviewCategory): void {
    this.checkedCategory = category.id;
    this.category = category;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getCategories();
  }
}
