import { Component, OnInit, ViewChild } from '@angular/core';
import { NgFor, NgIf } from '@angular/common';
import { RssFeed } from '../../../domain/entities/rssFeed';
import { RssService } from '../../services/rss.service';
import { Result } from '../../common/results/result';
import { RssFeedItem } from '../../../domain/entities/rssFeedItem';
import { FormsModule, NgForm } from '@angular/forms';
import { FeedItemModalComponent } from "../../shared/feed-item-modal/feed-item-modal.component";
import { TimeElapsedPipe } from '../../../infrastructure/utilities/time-elapsed.pipe';


@Component({
  selector: 'app-rss-feed-overview',
  standalone: true,
  imports: [NgFor, FormsModule, NgIf, FeedItemModalComponent,TimeElapsedPipe],
  templateUrl: './rss-feed-overview.component.html',
  styleUrl: './rss-feed-overview.component.scss'
})
export class RssFeedOverviewComponent implements OnInit {

  @ViewChild('formInput', { static: false }) formInput!: NgForm;

  selectedFeedItem: RssFeedItem | null = null;

  rssFeeds: RssFeed[] = [];
  rssFeedItems: RssFeedItem[] = [];

  constructor(private rssService: RssService) { }

  ngOnInit(): void {
    this.loadRssFeeds();

    this.rssService.feedAdded$.subscribe((feedAdded) => {
      if (feedAdded) {
        this.loadRssFeeds();
      }
    });
  }

  loadRssFeeds() {
    this.rssService.getAllRssFeeds().subscribe((result: Result) => {
      if (result.isSuccess) {
        this.rssFeeds = result.value.reverse();
        this.rssFeedItems = result.value;
        console.log('RSS Feeds fetched successfully:', this.rssFeeds);
      } else {
        console.error('Error fetching RSS feeds:', result.error);
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

  selectFeedItem(feedItem: RssFeedItem): void {
    this.selectedFeedItem = feedItem;
    console.log('Selected feed item:', feedItem);
  }
}
