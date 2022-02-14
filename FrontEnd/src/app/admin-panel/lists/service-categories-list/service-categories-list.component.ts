import { Component, OnInit } from '@angular/core';
import { ServiceCategory } from 'src/app/models/servicecategory.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-service-categories-list',
  templateUrl: './service-categories-list.component.html',
  styleUrls: ['./service-categories-list.component.css']
})
export class ServiceCategoriesListComponent implements OnInit {

  categories: ServiceCategory[] | null = null;
  category: string | null = null;
  checkedCategory: number | null = null;

  constructor() {}

  addCategory(): void {
    let category = {
      name: this.category,
    };

    fetch('https://localhost:44381/api/servicecategories/addcategory', {
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
        this.category = '';
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

    fetch('https://localhost:44381/api/servicecategories/editcategory', {
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
        this.category = '';
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

    fetch('https://localhost:44381/api/servicecategories/deletecategory', {
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
        this.category = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getCategories(): void {
    fetch('https://localhost:44381/api/servicecategories/getcategories', {
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

  setCategory(id: number | null, category: string): void {
    this.checkedCategory = id;
    this.category = category;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getCategories();
  }

}
