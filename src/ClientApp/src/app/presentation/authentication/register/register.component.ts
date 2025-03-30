import {Component, inject} from '@angular/core';
import {Router, RouterLink} from "@angular/router";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {NgIf} from "@angular/common";
import ValidateForm from "../../../infrastructure/utilities/validate-form";
import {AuthService} from "../../../infrastructure/services/auth-service";

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
  private router = inject(Router)
constructor(private formBuilder: FormBuilder) {
  this.registerForm = this.formBuilder.group({
    username: ['' , [Validators.required, Validators.minLength(3)]],
    email: ['' , [Validators.required, Validators.pattern(this.emailPattern)]],
    password: ['' , Validators.required],
    terms: [false, Validators.requiredTrue]
  });
}
  hideShowPassword(){
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "bi-eye" : this.eyeIcon = "bi-eye-slash";
    this.isText ? this.type = "text" : this.type = "password";
  }
  onSignUp() {
    if (this.registerForm.valid) {
      console.log(this.registerForm.value);
      // Send the obj to database
      this.authenticateService.register(this.registerForm.value).subscribe({
        next:(result) => {
            alert(result.value);
            this.registerForm.reset();
           this.router.navigate(['/login'])
        },
        error:(err) =>{
          alert(err?.error.message)
        }
      })

    } else{

      // throw error
      ValidateForm.validateAllFormFields(this.registerForm)
    }
  }
}
