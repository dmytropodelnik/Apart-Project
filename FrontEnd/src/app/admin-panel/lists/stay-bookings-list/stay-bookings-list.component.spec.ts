import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StayBookingsListComponent } from './stay-bookings-list.component';

describe('StayBookingsListComponent', () => {
  let component: StayBookingsListComponent;
  let fixture: ComponentFixture<StayBookingsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StayBookingsListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StayBookingsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
