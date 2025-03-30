import {RssFeed} from "./rss-feed";
import {UserRole} from "./user-role";

export interface User {
  id: number;
  username: string;
  email: string;
  password: string;

  rssFeedId: number;
  rssFeed: RssFeed;

  UserRoles: UserRole[];
  rssFeeds: RssFeed [];
}
