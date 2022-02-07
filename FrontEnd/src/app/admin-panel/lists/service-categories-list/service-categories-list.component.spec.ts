import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ServiceCategoriesListComponent } from './service-categories-list.component';

describe('ServiceCategoriesListComponent', () => {
  let component: ServiceCategoriesListComponent;
  let fixture: ComponentFixture<ServiceCategoriesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ServiceCategoriesListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ServiceCategoriesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
