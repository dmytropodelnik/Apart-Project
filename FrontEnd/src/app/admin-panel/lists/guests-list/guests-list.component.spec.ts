import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TempUsersListComponent } from './temp-users-list.component';

describe('TempUsersListComponent', () => {
  let component: TempUsersListComponent;
  let fixture: ComponentFixture<TempUsersListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TempUsersListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TempUsersListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
