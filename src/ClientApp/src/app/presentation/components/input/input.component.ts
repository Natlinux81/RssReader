import { Component } from '@angular/core';
import { RssFeedItem } from '../../../domain/entities/rssFeedItem';
import { RssFeed } from '../../../domain/entities/rssFeed';
import { rssFeeds } from '../../../mockdata/mock-rssFeed';

@Component({
  selector: 'app-input',
  standalone: true,
  imports: [],
  templateUrl: './input.component.html',
  styleUrl: './input.component.scss'
})
export class InputComponent {

  rssFeedItems: RssFeedItem[] = [
    {
      id: 1,
      title: "Tech News Today",
      link: "https://example.com/tech-news-today",
      description: "Latest updates in the world of technology.",
      publishDate: new Date("2024-09-01"),
      imageUrl: "pexels-markusspiske-3970330.jpg",
      rssFeedId: 1,
    },
    {
      id: 2,
      title: "Science Breakthroughs",
      link: "https://example.com/science-breakthroughs",
      description: "Recent discoveries in science and space exploration.",
      publishDate: new Date("2024-09-02"),
      imageUrl: "pexels-markusspiske-3970330.jpg",
      rssFeedId: 1,
    },
  ];

  rssFeed: RssFeed = {
    id: 1,
    url: "https://example.com/tech-rss",
    channelTitle: "Tech & Science News",
    feedItems: this.rssFeedItems,
  }


  addFeed() {
    console.log("addFeed");
    rssFeeds.push(this.rssFeed);

  }
}
