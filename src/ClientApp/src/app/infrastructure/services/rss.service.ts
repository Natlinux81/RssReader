import {inject, Injectable} from '@angular/core';
import {BehaviorSubject, Observable, tap} from 'rxjs';
import {RssFeedRequest} from '../../presentation/models/RssFeedRequest';
import {HttpClient} from '@angular/common/http';
import {IRssService} from '../../presentation/interfaces/IRssService';

@Injectable({
  providedIn: 'root'
})
export class RssService implements IRssService {
  httpClient = inject(HttpClient)

  // private baseUrl = 'api/rssFeeds';
  private baseUrl = 'https://localhost:7091/apiRssFeed';
  private feedAddedSubject = new BehaviorSubject<boolean>(false);

  feedAdded$ = this.feedAddedSubject.asObservable();

  addRssFeed(rssFeedRequest: RssFeedRequest, feedUrl: string): Observable<any> {
    const url = `${this.baseUrl}?feedUrl=${encodeURIComponent(feedUrl)}`;
    return this.httpClient.post<any>(url, rssFeedRequest).pipe(
        tap(() => this.feedAddedSubject.next(true))
    );
  }

  getAllRssFeeds(): Observable<any> {
    return this.httpClient.get<any>(this.baseUrl);
  }

  deleteRssFeed(id: number): Observable<any> {
    return this.httpClient.delete<any>(this.baseUrl + '/' + id);
  }

  getRssFeedById(id: number): Observable<any> {
    return this.httpClient.get<any>(this.baseUrl + '/' + id);
  }

  updateRssFeedItems(): Observable<any> {
    return this.httpClient.put<any>(this.baseUrl + '/update', null);
  }
}
