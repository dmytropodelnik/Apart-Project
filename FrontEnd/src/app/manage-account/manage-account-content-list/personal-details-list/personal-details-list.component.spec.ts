import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonalDetailsListComponent } from './personal-details-list.component';

describe('PersonalDetailsListComponent', () => {
  let component: PersonalDetailsListComponent;
  let fixture: ComponentFixture<PersonalDetailsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PersonalDetailsListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PersonalDetailsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
