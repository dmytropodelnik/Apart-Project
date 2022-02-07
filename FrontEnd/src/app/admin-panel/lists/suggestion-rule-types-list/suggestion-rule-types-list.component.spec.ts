import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SuggestionRuleTypesListComponent } from './suggestion-rule-types-list.component';

describe('SuggestionRuleTypesListComponent', () => {
  let component: SuggestionRuleTypesListComponent;
  let fixture: ComponentFixture<SuggestionRuleTypesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SuggestionRuleTypesListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SuggestionRuleTypesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
