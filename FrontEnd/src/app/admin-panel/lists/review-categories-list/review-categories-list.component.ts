import { Component, OnInit } from '@angular/core';
import { ReviewCategory } from 'src/app/models/Review/reviewcategory.item';
import { AdminContentService } from 'src/app/services/admin-content.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-review-categories-list',
  templateUrl: './review-categories-list.component.html',
  styleUrls: ['./review-categories-list.component.css']
})
export class ReviewCategoriesListComponent implements OnInit {

  categories: ReviewCategory[] | null = null;
  category: ReviewCategory | null = null;
  searchCategory: string = '';
  checkedCategory: number | null = null;

  page: number = 1;
  pageSize: number = 15;

  constructor(
    private adminContentService: AdminContentService
  ) {

  }

  search(): void {
    fetch('https://localhost:44381/api/reviewcategories/search?category=' + this.searchCategory, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
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
        alert(ex);
      });
  }

  addCategory(): void {
    let category = {
      name: this.category,
    };

    fetch('https://localhost:44381/api/categories/addcategory', {
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
        this.category = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editCategory(): void {
    let category = {
      id: this.checkedCategory,
      name: this.category,
    };

    fetch('https://localhost:44381/api/categories/editcategory', {
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
        this.category = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteCategory(): void {
    let category = {
      id: this.checkedCategory,
      name: this.category,
    };

    fetch('https://localhost:44381/api/categories/deletecategory', {
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
          alert('Editing error!');
        }
        this.category = null;
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getCategories(): void {
    fetch(`https://localhost:44381/api/reviewcategories/getcategories?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.categories = data.categories;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  collectElements(categories: ReviewCategory[]): void {
    for (let item of categories) {
      this.categories?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(`https://localhost:44381/api/reviewcategories/getcategories?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.categories);
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setCategory(category : ReviewCategory): void {
    this.checkedCategory = category.id;
    this.category = category;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getCategories();
  }

}
