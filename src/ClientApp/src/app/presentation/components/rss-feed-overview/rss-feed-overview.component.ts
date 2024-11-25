import {Component, inject, OnInit} from '@angular/core';
import {NgFor, NgOptimizedImage} from '@angular/common';
import {RssFeed} from '../../../domain/entities/rssFeed';
import {RssService} from '../../../infrastructure/services/rss.service';
import {RssFeedItem} from '../../../domain/entities/rssFeedItem';
import {FormsModule} from '@angular/forms';
import {FeedItemModalComponent} from "../../shared/feed-item-modal/feed-item-modal.component";
import {TimeElapsedPipe} from '../../../infrastructure/utilities/time-elapsed.pipe';
import {UpdateFeedItemsService} from "../../../infrastructure/services/update-feed-items.service";

@Component({
  selector: 'app-rss-feed-overview',
  imports: [NgFor, FormsModule, FeedItemModalComponent, TimeElapsedPipe, NgOptimizedImage],
  templateUrl: './rss-feed-overview.component.html',
  styleUrl: './rss-feed-overview.component.scss'
})
export class RssFeedOverviewComponent implements OnInit {
  rssService = inject(RssService)
  updateRssFeedItemsService = inject(UpdateFeedItemsService)

  selectedFeedItem: RssFeedItem | null = null;
  rssFeeds: RssFeed[] = [];

  ngOnInit(): void {
    this.fetchRssFeeds();

    this.rssService.feedAdded$.subscribe((feedAdded) => {
      if (feedAdded) {
        this.fetchRssFeeds();
      }
    });

    this.updateRssFeedItemsService.updateFeedItems()
  }
  fetchRssFeeds() {
    this.rssService.getAllRssFeeds().subscribe({
      next: (result) => {
        if (result.isSuccess) {
          this.rssFeeds = result.value.reverse();
          console.log('RSS Feeds fetched successfully:', this.rssFeeds);
        }
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
