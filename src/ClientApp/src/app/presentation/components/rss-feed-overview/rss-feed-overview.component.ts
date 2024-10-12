import { Component, OnInit } from '@angular/core';
import { NgFor } from '@angular/common';
import { RssFeed} from '../../../domain/entities/rssFeed';
import { InputComponent } from "../input/input.component";
import { RssService } from '../../services/rss.service';
import { Result } from '../../common/results/result';
import { RssFeedRequest } from '../../models/RssFeedRequest';
import { RssFeedItem } from '../../../domain/entities/rssFeedItem';


@Component({
  selector: 'app-rss-feed-overview',
  standalone: true,
  imports: [NgFor, InputComponent],
  templateUrl: './rss-feed-overview.component.html',
  styleUrl: './rss-feed-overview.component.scss'
})
export class RssFeedOverviewComponent implements OnInit{
  constructor(private rssService : RssService) {}

  rssFeeds : RssFeed[] = [];

  rssFeedItems : RssFeedItem[] = [];

  rssFeed: RssFeed = {
    id: 3,
    url: "https://example.com/tech-rss",
    channelTitle: "Test Feed",
    feedItems: []
  }

  ngOnInit(): void {
    this.loadRssFeeds();
  }

  loadRssFeeds() {
    this.rssService.getAllRssFeeds().subscribe((result: Result) => {
      if (result.isSuccess) {
        this.rssFeeds = result.value;
        this.rssFeedItems = result.value;
        console.log('RSS Feeds fetched successfully:', this.rssFeeds);
      } else {
        console.error('Error fetching RSS feeds:', result.error);
      }
    });
  }

  addRssFeed() {
    const newFeed: RssFeedRequest = { channelTitle: 'New Feed', url: 'https://example.com/feed', feedItems: [] };
    const feedUrl = 'https://example.com/feed'; // Beispiel-Feed-URL
    this.rssService.addRssFeed(newFeed, feedUrl).subscribe((result: Result) => {
      if (result.isSuccess) {
        console.log('RSS Feed added successfully');
      } else {
        console.error('Error adding RSS feed:', result.error);
      }
    });
  }

  deleteRssFeed(id: number) {
    this.rssService.deleteRssFeed(id).subscribe((result: Result) => {
      if (result.isSuccess) {
        this.rssFeeds = this.rssFeeds.filter(feed => feed.id !== id);
        console.log('RSS Feed deleted successfully');
      } else {
        console.error('Error deleting RSS feed:', result.error);
      }
    });
  }
}
