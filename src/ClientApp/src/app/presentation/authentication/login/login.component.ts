import { Component } from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {RouterLink} from "@angular/router";
import {NgIf, NgOptimizedImage} from "@angular/common";
import ValidateForm from "../../../infrastructure/utilities/validate-form";

@Component({
  selector: 'app-login',
  imports: [
    ReactiveFormsModule,
    RouterLink,
    NgIf,
    NgOptimizedImage
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  loginForm: FormGroup;
  type: string = "password";
  isText: boolean = true;
  eyeIcon: string = "bi-eye-slash"
  constructor(private formBuilder: FormBuilder) {
    this.loginForm = this.formBuilder.group({
      username: ['' , [Validators.required, Validators.minLength(3)]],
      password: ['' , Validators.required],
    });
  }

  hideShowPassword() {
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "bi-eye" : this.eyeIcon = "bi-eye-slash";
    this.isText ? this.type = "text" : this.type = "password";
  }

  openPopup() {
    // TODO: open popup for registration
  }

  onSignIn() {
    if (this.loginForm.valid) {
      //
      // // Send the obj to database
      // this.authenticateService.signIn(this.loginForm.value).subscribe({
      //   next: (result) => {
      //     this.loginForm.reset();
      //     this.authenticateService.storeToken(result.accessToken);
      //     this.authenticateService.storeRefreshToken(result.refreshToken)
      //     const tokenPayload = this.authenticateService.decodedToken();
      //     this.userStore.setUsernameForStore(tokenPayload.name);
      //     this.userStore.setRoleForStore(tokenPayload.role);
      //     this.router.navigate(['dashboard'])
      //
      //   },
      //   error: (err) => {
      //     alert("Username or Password wrong")
      //   }
      // })

    } else {
      // throw error
      ValidateForm.validateAllFormFields(this.loginForm)
    }
  }
}
