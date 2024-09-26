import { Injectable } from '@angular/core';
import { IRssService } from '../interfaces/IRssService';
import { RssFeedRequest } from '../models/RssFeedRequest';
import { Observable } from 'rxjs';
import { RssFeed } from '../../domain/entities/rssFeed';

@Injectable({
  providedIn: 'root'
})
export class RssServiceService  implements IRssService {
  addRssFeed(rssFeedRequest: RssFeedRequest, url: string): Observable<RssFeed> {
    throw new Error('Method not implemented.');
  }
}
