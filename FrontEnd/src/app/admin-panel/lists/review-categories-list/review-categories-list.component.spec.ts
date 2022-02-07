import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReviewCategoriesListComponent } from './review-categories-list.component';

describe('ReviewCategoriesListComponent', () => {
  let component: ReviewCategoriesListComponent;
  let fixture: ComponentFixture<ReviewCategoriesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReviewCategoriesListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReviewCategoriesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
