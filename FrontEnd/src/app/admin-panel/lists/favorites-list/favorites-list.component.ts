import { Component, OnInit } from '@angular/core';
import { Favorite } from 'src/app/models/UserData/favorite.item';

@Component({
  selector: 'app-favorites-list',
  templateUrl: './favorites-list.component.html',
  styleUrls: ['./favorites-list.component.css']
})
export class FavoritesListComponent implements OnInit {

  favorites: Favorite[] | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}
