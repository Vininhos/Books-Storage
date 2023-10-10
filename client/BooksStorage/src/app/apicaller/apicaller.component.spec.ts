import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApicallerComponent } from './apicaller.component';

describe('ApicallerComponent', () => {
  let component: ApicallerComponent;
  let fixture: ComponentFixture<ApicallerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ApicallerComponent]
    });
    fixture = TestBed.createComponent(ApicallerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
