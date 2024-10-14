import { Observable } from "rxjs";
import { RssFeedRequest } from "../models/RssFeedRequest";
import { RssFeed } from '../../domain/entities/rssFeed';
import { Result } from "../common/results/result";

export interface IRssService {

    addRssFeed(rssFeedRequest: RssFeedRequest, url: string): Observable<Result>;

    getAllRssFeeds(): Observable<Result>;

    deleteRssFeed(id: number): Observable<Result>;
    
    getRssFeedById(id: number): Observable<Result>;
}
