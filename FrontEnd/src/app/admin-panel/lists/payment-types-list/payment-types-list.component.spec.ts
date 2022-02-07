import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaymentTypesListComponent } from './payment-types-list.component';

describe('PaymentTypesListComponent', () => {
  let component: PaymentTypesListComponent;
  let fixture: ComponentFixture<PaymentTypesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PaymentTypesListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PaymentTypesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
