import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookingPricesListComponent } from './booking-prices-list.component';

describe('BookingPricesListComponent', () => {
  let component: BookingPricesListComponent;
  let fixture: ComponentFixture<BookingPricesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BookingPricesListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BookingPricesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
