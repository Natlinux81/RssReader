import {Observable} from "rxjs";
import {RssFeedRequest} from "../models/RssFeedRequest";

export interface IRssService {

  addRssFeed(rssFeedRequest: RssFeedRequest, url: string): Observable<any>;

  getAllRssFeeds(): Observable<any>;

  deleteRssFeed(id: number): Observable<any>;

  getRssFeedById(id: number): Observable<any>;
}
