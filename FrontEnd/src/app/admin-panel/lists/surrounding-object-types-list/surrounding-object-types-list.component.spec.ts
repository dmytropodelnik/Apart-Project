import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SurroundingObjectTypesListComponent } from './surrounding-object-types-list.component';

describe('SurroundingObjectTypesListComponent', () => {
  let component: SurroundingObjectTypesListComponent;
  let fixture: ComponentFixture<SurroundingObjectTypesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SurroundingObjectTypesListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SurroundingObjectTypesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
