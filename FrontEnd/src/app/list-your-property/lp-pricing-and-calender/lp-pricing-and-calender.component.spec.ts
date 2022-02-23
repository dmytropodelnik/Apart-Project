import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LpPricingAndCalenderComponent } from './lp-pricing-and-calender.component';

describe('LpPricingAndCalenderComponent', () => {
  let component: LpPricingAndCalenderComponent;
  let fixture: ComponentFixture<LpPricingAndCalenderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LpPricingAndCalenderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LpPricingAndCalenderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
