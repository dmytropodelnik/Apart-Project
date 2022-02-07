import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FileModelsListComponent } from './file-models-list.component';

describe('FileModelsListComponent', () => {
  let component: FileModelsListComponent;
  let fixture: ComponentFixture<FileModelsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FileModelsListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FileModelsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
