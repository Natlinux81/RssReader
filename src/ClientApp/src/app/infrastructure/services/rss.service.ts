import {inject, Injectable} from '@angular/core';
import {BehaviorSubject, Observable, tap} from 'rxjs';
import {RssFeedRequest} from '../../presentation/models/rss-feed-request';
import {HttpClient} from '@angular/common/http';
import {IRssService} from '../../presentation/interfaces/irss-service';
import {RssFeed} from "../../domain/entities/rss-feed";
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class RssService implements IRssService {
  httpClient = inject(HttpClient)

  private baseUrl = environment.baseUrl;

  private feedAddedSubject = new BehaviorSubject<boolean>(false);
  feedAdded$ = this.feedAddedSubject.asObservable();

  private rssFeedsSubject = new BehaviorSubject<RssFeed[]>([]);
  rssFeeds$ = this.rssFeedsSubject.asObservable();

  addRssFeed(rssFeedRequest: RssFeedRequest, feedUrl: string): Observable<any> {
    const url = `${this.baseUrl + 'rssFeed'}?feedUrl=${encodeURIComponent(feedUrl)}`;
    return this.httpClient.post<any>(url, rssFeedRequest).pipe(
        tap(() => this.feedAddedSubject.next(true))
    );
  }

  loadRssFeeds(): void {
    this.getAllRssFeeds().subscribe({
      next: (result) => {
        if (result.isSuccess) {
          this.rssFeedsSubject.next(result.value.reverse());
          console.log('RSS Feeds loaded into BehaviorSubject:', result.value);
        }
      },
      error: (err) => {
        console.error('Error loading RSS feeds:', err);
      }
    });
  }
  getAllRssFeeds(): Observable<any> {
    return this.httpClient.get<any>(this.baseUrl + 'rssFeed');
  }

  deleteRssFeed(id: number): Observable<any> {
    return this.httpClient.delete<any>(this.baseUrl + 'rssFeed/' + id);
  }

  getRssFeedById(id: number): Observable<any> {
    return this.httpClient.get<any>(this.baseUrl + 'rssFeed/' + id);
  }

  updateRssFeedItems(): Observable<any> {
    return this.httpClient.put<any>(this.baseUrl + 'rssFeed/update', null);
  }
}
