import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlightClassTypesListComponent } from './flight-class-types-list.component';

describe('FlightClassTypesListComponent', () => {
  let component: FlightClassTypesListComponent;
  let fixture: ComponentFixture<FlightClassTypesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlightClassTypesListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlightClassTypesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
