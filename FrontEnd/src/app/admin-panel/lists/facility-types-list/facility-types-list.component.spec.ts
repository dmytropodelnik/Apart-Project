import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FacilityTypesListComponent } from './facility-types-list.component';

describe('FacilityTypesListComponent', () => {
  let component: FacilityTypesListComponent;
  let fixture: ComponentFixture<FacilityTypesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FacilityTypesListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FacilityTypesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
