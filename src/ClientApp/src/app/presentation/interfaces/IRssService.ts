import { Observable } from "rxjs";
import { RssFeedRequest } from "../models/RssFeedRequest";
import { RssFeed } from '../../domain/entities/rssFeed';

export interface IRssService {

    addRssFeed(rssFeedRequest: RssFeedRequest, url: string): Observable<RssFeed>;

    getAllRssFeeds(): Observable<RssFeed[]>;

    deleteRssFeed(id: string): Observable<RssFeed>;
}
