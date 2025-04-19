import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private httpClient = inject(HttpClient)
  private baseUrl = environment.baseUrl

  constructor() {
  }

  getAllUsers() {
    return this.httpClient.get<any>(this.baseUrl + 'user');
  }
}
