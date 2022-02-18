import { Component, OnInit } from '@angular/core';
import { BookingCategory } from 'src/app/models/bookingcategory.item';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-booking-categories-list',
  templateUrl: './booking-categories-list.component.html',
  styleUrls: ['./booking-categories-list.component.css']
})
export class BookingCategoriesListComponent implements OnInit {
  categories: BookingCategory[] | null = null;
  category: BookingCategory;
  searchCategory: string = '';
  checkedCategory: number | null = null;

  constructor() {
    this.category = new BookingCategory();
  }

  search(): void {
    fetch('https://localhost:44381/api/bookingcategories/search?category=' + this.searchCategory, {
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

    fetch('https://localhost:44381/api/bookingcategories/addcategory', {
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

    fetch('https://localhost:44381/api/bookingcategories/editcategory', {
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

    fetch('https://localhost:44381/api/bookingcategories/deletecategory', {
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
          alert('Deleting error!');
        }
        this.category.category = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getCategories(): void {
    fetch('https://localhost:44381/api/bookingcategories/getcategories', {
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
