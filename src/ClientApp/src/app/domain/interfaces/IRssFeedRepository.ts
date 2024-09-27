import { RssFeed } from "../entities/rssFeed";

export interface IRssFeedRepository {
    // extra implementations
    getByUrlAsync(url: string): Promise<RssFeed>;

    readRssFeed(url: URL): Promise<RssFeed>;
}
