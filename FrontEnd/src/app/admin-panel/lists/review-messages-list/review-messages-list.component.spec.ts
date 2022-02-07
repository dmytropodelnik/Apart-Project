import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReviewMessagesListComponent } from './review-messages-list.component';

describe('ReviewMessagesListComponent', () => {
  let component: ReviewMessagesListComponent;
  let fixture: ComponentFixture<ReviewMessagesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReviewMessagesListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReviewMessagesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
