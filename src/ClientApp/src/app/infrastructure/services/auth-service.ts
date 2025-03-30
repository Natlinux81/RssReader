import {IAuthService} from "../../presentation/interfaces/iauth-service";
import {LoginRequest} from "../../presentation/models/login-request";
import {Observable} from "rxjs";
import {RegisterRequest} from "../../presentation/models/register-request";
import {inject, Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class AuthService implements IAuthService  {
  httpClient = inject(HttpClient)

  private baseUrl = environment.baseUrl

  register(registerRequest: RegisterRequest): Observable<any> {
    return this.httpClient.post<any>(`${this.baseUrl}auth/register`, registerRequest);
  }
  login(loginRequest: LoginRequest): Observable<any> {
    return this.httpClient.post<any>(`${this.baseUrl}auth/login`, loginRequest);

  }
}
