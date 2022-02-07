import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AttractionBookingsListComponent } from './attraction-bookings-list.component';

describe('AttractionBookingsListComponent', () => {
  let component: AttractionBookingsListComponent;
  let fixture: ComponentFixture<AttractionBookingsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AttractionBookingsListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AttractionBookingsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
