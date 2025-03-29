import {RssFeedItemRequest} from "./rss-feed-item-request";

export class RssFeedRequest {
  constructor(public url: string, public channelTitle: string, public feedItems: RssFeedItemRequest[]) {
  }
}
