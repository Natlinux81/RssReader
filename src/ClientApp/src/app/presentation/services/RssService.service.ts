import { RssFeedRepository } from './../../infrastructure/repositories/RssFeedRepository';
import { Injectable } from '@angular/core';
import { IRssService } from '../interfaces/IRssService';
import { RssFeedRequest } from '../models/RssFeedRequest';
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RssServiceService implements IRssService {

  constructor(private httpClient : HttpClient) { }

  baseUrl = 'http://localhost:5000/';

  addRssFeed(url: string, cancellationToken: AbortSignal): Observable<RssFeedRequest> {
    var response = this.httpClient.post<RssFeedRequest>(this.baseUrl + url, cancellationToken);
    return response;
  }
}
