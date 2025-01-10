import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForgetPasswordPopupComponent } from './forget-password-popup.component';

describe('ForgetPasswordPopupComponent', () => {
  let component: ForgetPasswordPopupComponent;
  let fixture: ComponentFixture<ForgetPasswordPopupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ForgetPasswordPopupComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ForgetPasswordPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
