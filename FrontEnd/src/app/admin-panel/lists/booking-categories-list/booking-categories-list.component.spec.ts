import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookingCategoriesListComponent } from './booking-categories-list.component';

describe('BookingCategoriesListComponent', () => {
  let component: BookingCategoriesListComponent;
  let fixture: ComponentFixture<BookingCategoriesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BookingCategoriesListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BookingCategoriesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
