import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmailNotificationsListComponent } from './email-notifications-list.component';

describe('EmailNotificationsListComponent', () => {
  let component: EmailNotificationsListComponent;
  let fixture: ComponentFixture<EmailNotificationsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmailNotificationsListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmailNotificationsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
