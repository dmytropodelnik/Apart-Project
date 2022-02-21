import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LpPhotosComponent } from './lp-photos.component';

describe('LpPhotosComponent', () => {
  let component: LpPhotosComponent;
  let fixture: ComponentFixture<LpPhotosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LpPhotosComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LpPhotosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
