import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PromocodeGeneratorComponent } from './promocode-generator.component';

describe('PromocodeGeneratorComponent', () => {
  let component: PromocodeGeneratorComponent;
  let fixture: ComponentFixture<PromocodeGeneratorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PromocodeGeneratorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PromocodeGeneratorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
