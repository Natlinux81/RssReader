import {Component, OnInit} from '@angular/core';
import {NgFor, NgOptimizedImage} from '@angular/common';
import {RssFeed} from '../../../domain/entities/rssFeed';
import {RssService} from '../../services/rss.service';
import {RssFeedItem} from '../../../domain/entities/rssFeedItem';
import {FormsModule} from '@angular/forms';
import {FeedItemModalComponent} from "../../shared/feed-item-modal/feed-item-modal.component";
import {TimeElapsedPipe} from '../../../infrastructure/utilities/time-elapsed.pipe';
import {UpdateFeedItemsService} from "../../services/update-feed-items.service";

@Component({
  selector: 'app-rss-feed-overview',
  imports: [NgFor, FormsModule, FeedItemModalComponent, TimeElapsedPipe, NgOptimizedImage],
  templateUrl: './rss-feed-overview.component.html',
  styleUrl: './rss-feed-overview.component.scss'
})
export class RssFeedOverviewComponent implements OnInit {

  selectedFeedItem: RssFeedItem | null = null;
  rssFeeds: RssFeed[] = [];
  rssFeedItems: RssFeedItem[] = [];

  constructor(private rssService: RssService,
              private updateRssFeedItemsService: UpdateFeedItemsService) {
  }

  ngOnInit(): void {
    this.loadRssFeeds();

    this.rssService.feedAdded$.subscribe((feedAdded) => {
      if (feedAdded) {
        this.loadRssFeeds();
      }
    });

    this.updateRssFeedItemsService.updateFeedItems()
  }

  loadRssFeeds() {
    this.rssService.getAllRssFeeds().subscribe((result) => {
      if (result.isSuccess) {
        this.rssFeeds = result.value.reverse();
        this.rssFeedItems = result.value;
        console.log('RSS Feeds fetched successfully:', this.rssFeeds, result);
      } else {
        console.error('Error fetching RSS feeds:', result.error);
      }
    });
  }

  deleteRssFeed(id: number) {
    this.rssService.deleteRssFeed(id).subscribe((result) => {
      if (result.isSuccess) {
        this.rssFeeds = this.rssFeeds.filter(feed => feed.id !== id);
        console.log('RSS Feed deleted successfully', result);
      } else {
        console.error('Error deleting RSS feed:', result.error);
      }
    });
  }

  selectFeedItem(feedItem: RssFeedItem): void {
    this.selectedFeedItem = feedItem;
    console.log('Selected feed item:', feedItem);
  }
}
