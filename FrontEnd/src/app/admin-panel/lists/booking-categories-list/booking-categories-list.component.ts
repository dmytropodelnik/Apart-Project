import { Component, OnInit } from '@angular/core';
import { BookingCategory } from 'src/app/models/bookingcategory.item';

import AuthHelper from '../../../utils/authHelper';

@Component({
  selector: 'app-booking-categories-list',
  templateUrl: './booking-categories-list.component.html',
  styleUrls: ['./booking-categories-list.component.css']
})
export class BookingCategoriesListComponent implements OnInit {
  categories: BookingCategory[] | null = null;
  category: string | null = null;
  checkedCategory: number | null = null;

  constructor() {}

  addCategory(): void {
    let category = {
      name: this.category,
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

    fetch('https://localhost:44381/api/bookingcategory/editcategory', {
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

    fetch('https://localhost:44381/api/roles/deleterole', {
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
    fetch('https://localhost:44381/api/bookingcategories/getroles', {
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
