import {RssFeedItemRequest} from "./RssFeedItemRequest";

export class RssFeedRequest {
  constructor(public url: string, public channelTitle: string, public feedItems: RssFeedItemRequest[]) {
  }
}
