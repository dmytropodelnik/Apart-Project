import { Component, OnInit } from '@angular/core';
import { ServiceCategory } from 'src/app/models/servicecategory.item';

@Component({
  selector: 'app-service-categories-list',
  templateUrl: './service-categories-list.component.html',
  styleUrls: ['./service-categories-list.component.css']
})
export class ServiceCategoriesListComponent implements OnInit {

  categories: ServiceCategory[] | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}
