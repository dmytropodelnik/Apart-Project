import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardTypesListComponent } from './card-types-list.component';

describe('CardTypesListComponent', () => {
  let component: CardTypesListComponent;
  let fixture: ComponentFixture<CardTypesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CardTypesListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CardTypesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
