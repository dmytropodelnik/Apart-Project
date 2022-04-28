import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LpApartmentsComponent } from './lp-apartments.component';

describe('LpApartmentsComponent', () => {
  let component: LpApartmentsComponent;
  let fixture: ComponentFixture<LpApartmentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LpApartmentsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LpApartmentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
