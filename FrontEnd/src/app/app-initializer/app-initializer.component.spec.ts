import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppInitializerComponent } from './app-initializer.component';

describe('AppInitializerComponent', () => {
  let component: AppInitializerComponent;
  let fixture: ComponentFixture<AppInitializerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AppInitializerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AppInitializerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
