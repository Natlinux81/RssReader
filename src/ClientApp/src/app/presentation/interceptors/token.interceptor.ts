import {HttpErrorResponse, HttpInterceptorFn} from '@angular/common/http';
import {inject} from "@angular/core";
import {AuthService} from "../../infrastructure/services/auth-service";
import {catchError, throwError} from "rxjs";
import {ToastService} from "../../infrastructure/services/toast.service";
import { Router } from '@angular/router';

export const tokenInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const token = authService.getToken();
  const toastService = inject(ToastService);
  const router = inject(Router);

  // If a token exists, add it to the header
  if (token) {
    const authReq = req.clone({
      headers: req.headers.set('Authorization', `Bearer ${token}`)
    });

    return next(authReq).pipe(
      catchError((error) => {
        // If 401 Unauthorized, then navigate to the login page
        if (error instanceof HttpErrorResponse && error.status === 401) {

          // Remove token from storage
          authService.signOut();

          // navigate to the login page
            router.navigate(['/login']).then(success => {
              if (success) {
                toastService.show("Your token is expired or invalid. please login again.", {
                  classname: 'bg-warning text-dark',
                  delay: 7000
                });
              } else {
                toastService.show("something went wrong. please try again later.", {
                  classname: 'bg-danger text-light',
                  delay: 7000
                });
              }
            } )
        }

        // Pass on errors so that other handlers can process them
        return throwError(() => error);
      })
    );
  }

  //  If no token is available, forward the request unchanged
  return next(req);
};
