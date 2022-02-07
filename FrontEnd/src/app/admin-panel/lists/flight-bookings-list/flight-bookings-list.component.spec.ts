import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlightBookingsListComponent } from './flight-bookings-list.component';

describe('FlightBookingsListComponent', () => {
  let component: FlightBookingsListComponent;
  let fixture: ComponentFixture<FlightBookingsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlightBookingsListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlightBookingsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
