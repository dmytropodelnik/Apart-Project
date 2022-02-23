import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LpNameAndLocationComponent } from './lp-name-and-location.component';

describe('LpNameAndLocationComponent', () => {
  let component: LpNameAndLocationComponent;
  let fixture: ComponentFixture<LpNameAndLocationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LpNameAndLocationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LpNameAndLocationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
