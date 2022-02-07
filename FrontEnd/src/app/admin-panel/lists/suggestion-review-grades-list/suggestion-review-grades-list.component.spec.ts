import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SuggestionReviewGradesListComponent } from './suggestion-review-grades-list.component';

describe('SuggestionReviewGradesListComponent', () => {
  let component: SuggestionReviewGradesListComponent;
  let fixture: ComponentFixture<SuggestionReviewGradesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SuggestionReviewGradesListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SuggestionReviewGradesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
