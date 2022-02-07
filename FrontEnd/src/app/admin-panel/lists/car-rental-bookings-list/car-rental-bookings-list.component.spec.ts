import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarRentalBookingsListComponent } from './car-rental-bookings-list.component';

describe('CarRentalBookingsListComponent', () => {
  let component: CarRentalBookingsListComponent;
  let fixture: ComponentFixture<CarRentalBookingsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CarRentalBookingsListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CarRentalBookingsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
