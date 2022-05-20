import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LetterCreatorComponent } from './letter-creator.component';

describe('LetterCreatorComponent', () => {
  let component: LetterCreatorComponent;
  let fixture: ComponentFixture<LetterCreatorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LetterCreatorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LetterCreatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
