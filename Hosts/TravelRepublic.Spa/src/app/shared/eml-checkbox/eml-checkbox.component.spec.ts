import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmlCheckboxComponent } from './eml-checkbox.component';

describe('EmlCheckboxComponent', () => {
  let component: EmlCheckboxComponent;
  let fixture: ComponentFixture<EmlCheckboxComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmlCheckboxComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmlCheckboxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
