import {Component, inject} from '@angular/core';
import {Router, RouterLink} from "@angular/router";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {NgIf} from "@angular/common";
import ValidateForm from "../../../infrastructure/utilities/validate-form";
import {AuthService} from "../../../infrastructure/services/auth-service";
import {ToastService} from "../../../infrastructure/services/toast.service";

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

  authenticateService = inject(AuthService)
  toastService = inject(ToastService);
  private router = inject(Router)

  constructor(private formBuilder: FormBuilder) {
    this.registerForm = this.formBuilder.group({
      username: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.pattern(this.emailPattern)]],
      password: ['', Validators.required],
      terms: [false, Validators.requiredTrue]
    });
  }

  hideShowPassword() {
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "bi-eye" : this.eyeIcon = "bi-eye-slash";
    this.isText ? this.type = "text" : this.type = "password";
  }

  onSignUp() {
    if (this.registerForm.valid) {
      console.log(this.registerForm.value);
      // Send the obj to database
      this.authenticateService.register(this.registerForm.value).subscribe({
        next: (result) => {
          this.toastService.show(result.value, {
            classname: 'bg-success text-light',
            delay: 2000
          });
          this.registerForm.reset();
          this.router.navigate(['/login'])
        },
        error: (err) => {
          console.error(err?.error.error.message);
          this.toastService.show(err.error.error.message, {
            classname: 'bg-danger text-light',
            delay: 7000
          });
        }
      })

    } else {

      // throw error
      ValidateForm.validateAllFormFields(this.registerForm)
    }
  }
}
