import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JoinAsPartnerComponent } from './join-as-partner.component';

describe('JoinAsPartnerComponent', () => {
  let component: JoinAsPartnerComponent;
  let fixture: ComponentFixture<JoinAsPartnerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JoinAsPartnerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(JoinAsPartnerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
