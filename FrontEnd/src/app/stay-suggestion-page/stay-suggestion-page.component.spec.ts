import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StaySuggestionPageComponent } from './stay-suggestion-page.component';

describe('StaySuggestionPageComponent', () => {
  let component: StaySuggestionPageComponent;
  let fixture: ComponentFixture<StaySuggestionPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StaySuggestionPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StaySuggestionPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
