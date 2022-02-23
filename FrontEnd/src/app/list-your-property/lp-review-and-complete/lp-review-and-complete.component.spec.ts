import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LpReviewAndCompleteComponent } from './lp-review-and-complete.component';

describe('LpReviewAndCompleteComponent', () => {
  let component: LpReviewAndCompleteComponent;
  let fixture: ComponentFixture<LpReviewAndCompleteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LpReviewAndCompleteComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LpReviewAndCompleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
