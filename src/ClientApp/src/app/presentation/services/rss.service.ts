import {Injectable} from '@angular/core';
import {BehaviorSubject, catchError, Observable, tap, throwError} from 'rxjs';
import {RssFeedRequest} from '../models/RssFeedRequest';
import {HttpClient} from '@angular/common/http';
import {IRssService} from '../interfaces/IRssService';
import {ErrorHandlerService} from "./error-handler.service";

@Injectable({
  providedIn: 'root'
})
export class RssService implements IRssService {

  // private baseUrl = 'api/rssFeeds';
  private baseUrl = 'https://localhost:7091/apiRssFeed';

  private feedAddedSubject = new BehaviorSubject<boolean>(false);

  feedAdded$ = this.feedAddedSubject.asObservable();

  constructor(private httpClient: HttpClient, private handleErrorService: ErrorHandlerService) {
  }

  addRssFeed(rssFeedRequest: RssFeedRequest, feedUrl: string): Observable<any> {
    const url = `${this.baseUrl}?feedUrl=${encodeURIComponent(feedUrl)}`;
    return this.httpClient.post<any>(url, rssFeedRequest).pipe(
      tap(() => this.feedAddedSubject.next(true)),
      catchError((error) => {
        this.handleErrorService.handleError(error);
        return throwError(() => new Error());
      })
    );
  }

  getAllRssFeeds(): Observable<any> {
    return this.httpClient.get<any>(this.baseUrl).pipe(
      catchError(async (error) => this.handleErrorService.handleError(error))
    );
  }

  deleteRssFeed(id: number): Observable<any> {
    return this.httpClient.delete<any>(this.baseUrl + '/' + id).pipe(
      catchError(async (error) => this.handleErrorService.handleError(error))
    );
  }

  getRssFeedById(id: number): Observable<any> {
    return this.httpClient.get<any>(this.baseUrl + '/' + id).pipe(
      catchError(async (error) => this.handleErrorService.handleError(error))
    );
  }

  updateRssFeedItems(): Observable<any> {
    return this.httpClient.put<any>(this.baseUrl + '/update', null).pipe(
      catchError(async (error) => this.handleErrorService.handleError(error))
    );
  }
}
