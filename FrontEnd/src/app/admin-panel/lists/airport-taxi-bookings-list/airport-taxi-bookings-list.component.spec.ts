import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AirportTaxiBookingsListComponent } from './airport-taxi-bookings-list.component';

describe('AirportTaxiBookingsListComponent', () => {
  let component: AirportTaxiBookingsListComponent;
  let fixture: ComponentFixture<AirportTaxiBookingsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AirportTaxiBookingsListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AirportTaxiBookingsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
