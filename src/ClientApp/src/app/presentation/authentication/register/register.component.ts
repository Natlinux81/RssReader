import { Component } from '@angular/core';
import {RouterLink} from "@angular/router";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-register',
  imports: [
    RouterLink,
    ReactiveFormsModule,
    NgIf
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  emailPattern = "^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$"
  type: string = "password";
  isText: boolean = true;
  eyeIcon: string = "bi-eye-slash"
  registerForm: FormGroup;
constructor(private formBuilder: FormBuilder) {
  this.registerForm = this.formBuilder.group({
    username: ['' , [Validators.required, Validators.minLength(3)]],
    email: ['' , [Validators.required, Validators.pattern(this.emailPattern)]],
    password: ['' , Validators.required],
    terms: ['', Validators.required]
  });
}
  hideShowPassword(){
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "bi-eye" : this.eyeIcon = "bi-eye-slash";
    this.isText ? this.type = "text" : this.type = "password";
  }
  onSignUp() {
    // TODO: Implement sign up functionality
  }
}
