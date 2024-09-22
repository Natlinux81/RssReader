import { RssFeedItem } from "./rssFeedItem";

export interface RssFeed {
  id: number;
  url: string;
  channelTitle: string;
  feedItems: RssFeedItem[];
}
