import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RssFeedRequest } from '../models/RssFeedRequest';
import { HttpClient } from '@angular/common/http';
import { Result } from '../common/results/result';

@Injectable({
  providedIn: 'root'
})
export class RssService {

  // private baseUrl = 'api/rssFeeds';
  private baseUrl = 'https://localhost:7091/apiRssFeed';

  constructor(private httpClient: HttpClient) { }

  addRssFeed(rssFeedRequest: RssFeedRequest, feedUrl: string): Observable<Result> {
    const url = `${this.baseUrl}?feedUrl=${encodeURIComponent(feedUrl)}`;
    return this.httpClient.post<Result>(url, rssFeedRequest)
  }

  getAllRssFeeds(): Observable<Result> {
    return this.httpClient.get<Result>(this.baseUrl)
  }

  deleteRssFeed(id: number): Observable<Result> {
    return this.httpClient.delete<Result>(this.baseUrl + '/' + id)
  }

  getRssFeedById(id: number): Observable<Result> {
    return this.httpClient.get<Result>(this.baseUrl + '/' + id)
  }
}
