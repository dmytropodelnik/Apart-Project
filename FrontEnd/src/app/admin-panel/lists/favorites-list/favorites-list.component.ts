import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Favorite } from 'src/app/models/UserData/favorite.item';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-favorites-list',
  templateUrl: './favorites-list.component.html',
  styleUrls: ['./favorites-list.component.css'],
})
export class FavoritesListComponent implements OnInit {
  favorites: Favorite[] | null = null;
  favorite: Favorite | null = null;
  searchFavorite: string = '';
  checkedFavorite: number | null = null;

  page: number = 1;
  pageSize: number = 10;

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
      'https://localhost:44381/api/favorites/search?favorite=' +
        this.searchFavorite,
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
          this.favorites = data.favorites;
        } else {
          this.showAlert('Search error!');
        }
        this.searchFavorite = '';
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  addFavorite(): void {
    let favorite = {
      name: this.favorite,
    };

    fetch('https://apartmain.azurewebsites.net/api/favorites/addfavorite', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(favorite),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getFavorites();
        } else {
          this.showAlert('Adding error!');
        }
        this.favorite = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  editFavorite(): void {
    let favorite = {
      id: this.checkedFavorite,
    };

    fetch('https://apartmain.azurewebsites.net/api/favorites/editfavorite', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(favorite),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getFavorites();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.favorite = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  deleteFavorite(): void {
    let favorite = {
      id: this.checkedFavorite,
      name: this.favorite,
    };

    fetch('https://apartmain.azurewebsites.net/api/favorites/deletefavorite', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(favorite),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getFavorites();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.favorite = null;
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  getFavorites(): void {
    fetch(
      `https://localhost:44381/api/favorites/getfavorites?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.favorites = data.favorites;
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  collectElements(favorites: Favorite[]): void {
    for (let item of favorites) {
      this.favorites?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(
      `https://localhost:44381/api/favorites/getfavorites?page=${this.page}&pageSize=${this.pageSize}`,
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
          this.collectElements(data.favorites);
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  setFavorite(favorite: Favorite): void {
    this.checkedFavorite = favorite.id;
    this.favorite = favorite;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getFavorites();
  }
}
