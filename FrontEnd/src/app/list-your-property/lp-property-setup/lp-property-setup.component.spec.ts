import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LpPropertySetupComponent } from './lp-property-setup.component';

describe('LpPropertySetupComponent', () => {
  let component: LpPropertySetupComponent;
  let fixture: ComponentFixture<LpPropertySetupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LpPropertySetupComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LpPropertySetupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
