import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageAccountContentListComponent } from './manage-account-content-list.component';

describe('ManageAccountContentListComponent', () => {
  let component: ManageAccountContentListComponent;
  let fixture: ComponentFixture<ManageAccountContentListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManageAccountContentListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageAccountContentListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
