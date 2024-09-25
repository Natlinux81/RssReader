import { Observable } from "rxjs";
import { RssFeed } from "../entities/rssFeed";
import { IGenericRepository } from "./IGenericRepository";

export interface IRssFeedRepository extends IGenericRepository<RssFeed> {
    // extra implementations
    getByUrlAsync(url: string): Promise<RssFeed | null>;

    readRssFeed(url: URL): Observable<RssFeed>;
}
