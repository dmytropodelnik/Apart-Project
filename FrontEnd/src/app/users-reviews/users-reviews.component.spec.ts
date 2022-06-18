import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersReviewsComponent } from './users-reviews.component';

describe('UsersReviewsComponent', () => {
  let component: UsersReviewsComponent;
  let fixture: ComponentFixture<UsersReviewsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UsersReviewsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UsersReviewsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
