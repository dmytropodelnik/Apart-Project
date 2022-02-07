import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SuggestionRulesListComponent } from './suggestion-rules-list.component';

describe('SuggestionRulesListComponent', () => {
  let component: SuggestionRulesListComponent;
  let fixture: ComponentFixture<SuggestionRulesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SuggestionRulesListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SuggestionRulesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
