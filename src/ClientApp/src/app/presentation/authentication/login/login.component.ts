import {Component, inject} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {Router, RouterLink} from "@angular/router";
import {NgIf, NgOptimizedImage} from "@angular/common";
import ValidateForm from "../../../infrastructure/utilities/validate-form";
import {AuthService} from "../../../infrastructure/services/auth-service";
import {ToastService} from "../../../infrastructure/services/toast.service";

@Component({
  selector: 'app-login',
  imports: [
    ReactiveFormsModule,
    RouterLink,
    NgIf,
    NgOptimizedImage
  ],
  templateUrl: './login.component.html',
  standalone: true,
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  emailPattern = "^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$"
  loginForm: FormGroup;
  type: string = "password";
  isText: boolean = true;
  eyeIcon: string = "bi-eye-slash"

  authenticateService = inject(AuthService)
  toastService = inject(ToastService);
  private router = inject(Router)

  constructor(private formBuilder: FormBuilder) {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.pattern(this.emailPattern)]],
      password: ['', Validators.required],
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

  onLogin() {
    if (this.loginForm.valid) {
      console.log(this.loginForm.value)
      // Send the obj to a database
      this.authenticateService.login(this.loginForm.value).subscribe({
        next: (result) => {
          this.loginForm.reset();
          this.authenticateService.storeToken(result.value.accessToken);

          // this.authenticateService.storeRefreshToken(result.refreshToken)
          // const tokenPayload = this.authenticateService.decodedToken();
          // this.userStore.setUsernameForStore(tokenPayload.name);
          // this.userStore.setRoleForStore(tokenPayload.role);
          this.router.navigate(['admin-dashboard']).then(success => {
            if (success) {
              this.toastService.show('Login successful', {
                classname: 'bg-success text-light',
                delay: 2000
              });
            } else {
              this.toastService.show('Login failed, please try again later', {
                classname: 'bg-danger text-light',
                delay: 2000
              });
            }
          })

        },
        error: (err) => {
          console.error(err.error.error.message);
          this.toastService.show(err.error.error.message, {
            classname: 'bg-danger text-light',
            delay: 7000
          });
        }
      })

    } else {
      // throw error
      ValidateForm.validateAllFormFields(this.loginForm)
    }
  }
}
