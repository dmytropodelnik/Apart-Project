import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SuggestionHighlightsListComponent } from './suggestion-highlights-list.component';

describe('SuggestionHighlightsListComponent', () => {
  let component: SuggestionHighlightsListComponent;
  let fixture: ComponentFixture<SuggestionHighlightsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SuggestionHighlightsListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SuggestionHighlightsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
