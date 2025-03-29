import {RssFeedRequest} from "../models/rss-feed-request";
import {Observable} from "rxjs";

export interface IRssService {
  addRssFeed(rssFeedRequest: RssFeedRequest, url: string): Observable<any>;

  getAllRssFeeds(): Observable<any>;

  deleteRssFeed(id: number): Observable<any>;

  getRssFeedById(id: number): Observable<any>;
}
