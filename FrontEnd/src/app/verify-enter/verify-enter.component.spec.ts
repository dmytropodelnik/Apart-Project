import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VerifyEnterComponent } from './verify-enter.component';

describe('VerifyEnterComponent', () => {
  let component: VerifyEnterComponent;
  let fixture: ComponentFixture<VerifyEnterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VerifyEnterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VerifyEnterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
