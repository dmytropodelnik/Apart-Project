import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OtherTravelersListComponent } from './other-travelers-list.component';

describe('OtherTravelersListComponent', () => {
  let component: OtherTravelersListComponent;
  let fixture: ComponentFixture<OtherTravelersListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OtherTravelersListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OtherTravelersListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
