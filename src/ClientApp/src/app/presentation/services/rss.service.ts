import { Injectable } from '@angular/core';
import { IRssService } from '../interfaces/IRssService';
import { catchError, Observable, throwError } from 'rxjs';
import { RssFeed } from '../../domain/entities/rssFeed';
import { RssFeedRequest } from '../models/RssFeedRequest';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Result } from '../common/results/result';

@Injectable({
  providedIn: 'root'
})
export class RssService {

    // private baseUrl = 'api/rssFeeds';
    private baseUrl = 'https://localhost:7091/apiRssFeed';

    constructor(private httpClient: HttpClient) { }

    addRssFeed(rssFeedRequest: RssFeedRequest, feedUrl: string): Observable<Result> {
      return this.httpClient
        .post<Result>(this.baseUrl, { rssFeedRequest, feedUrl })
        .pipe(catchError(this.handleError));
    }

    getAllRssFeeds(): Observable<Result> {
      return this.httpClient
        .get<Result>(this.baseUrl)
        .pipe(catchError(this.handleError));
    }

    deleteRssFeed(id: number): Observable<Result> {
      return this.httpClient
        .delete<Result>(this.baseUrl + '/' + id)
        .pipe(catchError(this.handleError));
    }

    private handleError(error: HttpErrorResponse) {
      // Fehlerbehandlung
      let errorMessage = 'An unknown error occurred!';
      if (error.error instanceof ErrorEvent) {
        // Client-seitiger Fehler
        errorMessage = `Error: ${error.error.message}`;
      } else {
        // Server-seitiger Fehler
        errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
      }
      return throwError(errorMessage);
    }
  }
