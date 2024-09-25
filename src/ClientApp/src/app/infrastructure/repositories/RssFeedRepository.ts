import { RssFeed } from './../../domain/entities/rssFeed';
import { GenericRepository } from "./GenericRepository";
import { IRssFeedRepository } from "../../domain/interfaces/IRssFeedRepository";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


export class RssFeedRepository extends GenericRepository<RssFeed> implements IRssFeedRepository {

  rssFeed: RssFeed[] = [];

  constructor(private httpClient: HttpClient) {
    super();
  }


  getByUrlAsync(url: string): Promise<RssFeed | null> {
    const response = this.rssFeed.find(x => x.url === url) || null;
    return Promise.resolve(response);
  }
  readRssFeed(url: URL): Observable<RssFeed> {
    const response = this.httpClient.get<RssFeed>(url.toString());
    return response;
  }
}
