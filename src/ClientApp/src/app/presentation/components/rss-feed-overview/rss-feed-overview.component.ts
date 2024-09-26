import { RssFeedItem } from '../../../domain/entities/rssFeedItem';
import { Component } from '@angular/core';
import { rssFeeds } from '../../../mockdata/mock-rssFeed';
import { NgFor } from '@angular/common';
import { RssFeed} from '../../../domain/entities/rssFeed';
import { InputComponent } from "../input/input.component";

@Component({
  selector: 'app-rss-feed-overview',
  standalone: true,
  imports: [NgFor, InputComponent],
  templateUrl: './rss-feed-overview.component.html',
  styleUrl: './rss-feed-overview.component.scss'
})
export class RssFeedOverviewComponent {


  rssFeedItems: RssFeedItem[] = [
    {
      id: 1,
      title: "Tech News Today",
      link: "https://example.com/tech-news-today",
      description: "Latest updates in the world of technology.",
      publishDate: new Date("2024-09-01"),
      imageUrl: "pexels-markusspiske-3970330.jpg",
      rssFeedId: 3,
    },
    {
      id: 2,
      title: "Science Breakthroughs",
      link: "https://example.com/science-breakthroughs",
      description: "Recent discoveries in science and space exploration.",
      publishDate: new Date("2024-09-02"),
      imageUrl: "pexels-markusspiske-3970330.jpg",
      rssFeedId: 3,
    },
  ];

  rssFeed: RssFeed = {
    id: 3,
    url: "https://example.com/tech-rss",
    channelTitle: "Tech & Science News",
    feedItems: this.rssFeedItems,
  }

  RssFeeds = rssFeeds;

  deleteFeed(rssFeed : RssFeed) : void {
    var index = rssFeeds.indexOf(rssFeed);
    console.log(index);
    rssFeeds.splice(index, 1);
    }
}
