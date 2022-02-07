import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserProfilesListComponent } from './user-profiles-list.component';

describe('UserProfilesListComponent', () => {
  let component: UserProfilesListComponent;
  let fixture: ComponentFixture<UserProfilesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserProfilesListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserProfilesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
