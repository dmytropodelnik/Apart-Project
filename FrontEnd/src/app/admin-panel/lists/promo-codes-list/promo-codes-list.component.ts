import { Component, OnInit } from '@angular/core';
import { PromoCode } from 'src/app/models/Payment/promocode.item';

@Component({
  selector: 'app-promo-codes-list',
  templateUrl: './promo-codes-list.component.html',
  styleUrls: ['./promo-codes-list.component.css']
})
export class PromoCodesListComponent implements OnInit {

  codes: PromoCode[] | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}
