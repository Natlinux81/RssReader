import { Observable } from "rxjs";
import { RssFeedRequest } from "../models/RssFeedRequest";

export interface IRssService {
    addRssFeed(url: string, cancellationToken: AbortSignal): Observable<RssFeedRequest>;
}
