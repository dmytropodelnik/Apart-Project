import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SurroundingObjectsListComponent } from './surrounding-objects-list.component';

describe('SurroundingObjectsListComponent', () => {
  let component: SurroundingObjectsListComponent;
  let fixture: ComponentFixture<SurroundingObjectsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SurroundingObjectsListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SurroundingObjectsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
