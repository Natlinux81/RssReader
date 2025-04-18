import {inject, Injectable} from '@angular/core';
import {CanActivate, Router} from '@angular/router';
import {AuthService} from "../services/auth-service";
import {ToastService} from "../services/toast.service";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationGuard implements CanActivate {
  toastService = inject(ToastService);
  private authenticateService = inject(AuthService)
  private router = inject(Router)

  canActivate(): boolean {
    if (this.authenticateService.isLoggedIn()) {
      return true;
    } else {
      this.toastService.show("Please Login first", {
        classname: 'bg-warning text-dark',
        delay: 2000
      });
      this.router.navigate(['/login'])
      return false;
    }
  }

}
