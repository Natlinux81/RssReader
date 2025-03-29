import {RssFeedItem} from "./rss-feed-item";

export interface RssFeed {
  id: number;
  url: string;
  channelTitle: string;
  feedItems: RssFeedItem[];
}
