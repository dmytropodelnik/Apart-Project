import { Component, OnInit } from '@angular/core';
import { ServiceCategory } from 'src/app/models/servicecategory.item';
import { AdminContentService } from 'src/app/services/admin-content.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-service-categories-list',
  templateUrl: './service-categories-list.component.html',
  styleUrls: ['./service-categories-list.component.css']
})
export class ServiceCategoriesListComponent implements OnInit {

  categories: ServiceCategory[] | null = null;
  category: ServiceCategory;
  searchCategory: string = '';
  checkedCategory: number | null = null;

  page: number = 1;
  pageSize: number = 15;

  constructor(
    private adminContentService: AdminContentService
  ) {
    this.category = new ServiceCategory();
  }

  search(): void {
    fetch('http://apartmain.azurewebsites.net/api/servicecategories/search?category=' + this.searchCategory, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
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
      category: this.category.category,
    };

    fetch('http://apartmain.azurewebsites.net/api/servicecategories/addcategory', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
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
        alert(ex);
      });
  }

  editCategory(): void {
    let category = {
      id: this.checkedCategory,
      category: this.category.category,
    };

    fetch('http://apartmain.azurewebsites.net/api/servicecategories/editcategory', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
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
        alert(ex);
      });
  }

  deleteCategory(): void {
    let category = {
      id: this.checkedCategory,
      category: this.category.category,
    };

    fetch('http://apartmain.azurewebsites.net/api/servicecategories/deletecategory', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
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
        alert(ex);
      });
  }

  getCategories(): void {
    fetch('http://apartmain.azurewebsites.net/api/servicecategories/getcategories', {
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

  collectElements(categories: ServiceCategory[]): void {
    for (let item of categories) {
      this.categories?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(`http://apartmain.azurewebsites.net/api/servicecategories/getcategories?page=${this.page}&pageSize=${this.pageSize}`, {
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

  setCategory(category: ServiceCategory): void {
    this.checkedCategory = category.id;
    this.category.category = category.category;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getCategories();
  }

}
