import {RegisterRequest} from "../models/register-request";
import {Observable} from "rxjs";
import {LoginRequest} from "../models/login-request";

export interface IAuthService {
  register(registerRequest: RegisterRequest): Observable<any>;

  login(loginRequest: LoginRequest): Observable<any>;
}
