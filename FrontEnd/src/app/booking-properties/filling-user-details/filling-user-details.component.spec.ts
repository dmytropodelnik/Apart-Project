import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FillingUserDetailsComponent } from './filling-user-details.component';

describe('FillingUserDetailsComponent', () => {
  let component: FillingUserDetailsComponent;
  let fixture: ComponentFixture<FillingUserDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FillingUserDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FillingUserDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
