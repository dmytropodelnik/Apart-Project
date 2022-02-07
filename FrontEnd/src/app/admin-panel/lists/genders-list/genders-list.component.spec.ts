import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GendersListComponent } from './genders-list.component';

describe('GendersListComponent', () => {
  let component: GendersListComponent;
  let fixture: ComponentFixture<GendersListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GendersListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GendersListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
