import { Injectable } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';
import { RssFeedRequest } from '../models/RssFeedRequest';
import { HttpClient } from '@angular/common/http';
import { Result } from '../common/results/result';
import { IRssService } from '../interfaces/IRssService';

@Injectable({
  providedIn: 'root'
})
export class RssService implements IRssService {

  // private baseUrl = 'api/rssFeeds';
  private baseUrl = 'https://localhost:7091/apiRssFeed';

  constructor(private httpClient: HttpClient) { }

  addRssFeed(rssFeedRequest: RssFeedRequest, feedUrl: string): Observable<Result> {
    const url = `${this.baseUrl}?feedUrl=${encodeURIComponent(feedUrl)}`;
    return this.httpClient.post<Result>(url, rssFeedRequest).pipe(
      catchError((error) => this.handleError<Result>('errorAddRssFeed', error))
    );
  }

  getAllRssFeeds(): Observable<Result> {
    return this.httpClient.get<Result>(this.baseUrl).pipe(
      catchError((error) => this.handleError<Result>('errorGetAllRssFeeds', error))
    );
  }

  deleteRssFeed(id: number): Observable<Result> {
    return this.httpClient.delete<Result>(this.baseUrl + '/' + id).pipe(
      catchError((error) => this.handleError<Result>('errorDeleteRssFeed', error))
    );
  }

  // Fehlerbehandlung
  private handleError<T>(operation: string, error: any): Observable<T> {
    // Fehler in der Konsole protokollieren
    console.error(`${operation} fehlgeschlagen: ${error.message}`);

    // Strukturierte Fehlerantwort zurückgeben
    const errorResult: Result = {
      isSuccess: false,
      isFailure: true,
      error: { code: 'InternalServerError', message: 'Ein Fehler ist aufgetreten, während Ihre Anfrage bearbeitet wurde.' }
    };

    return of(errorResult as T);
  }
}
