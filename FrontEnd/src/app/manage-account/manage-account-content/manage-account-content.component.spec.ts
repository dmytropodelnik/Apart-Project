import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageAccountContentComponent } from './manage-account-content.component';

describe('ManageAccountContentComponent', () => {
  let component: ManageAccountContentComponent;
  let fixture: ComponentFixture<ManageAccountContentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManageAccountContentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageAccountContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
