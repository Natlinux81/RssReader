import { Component } from '@angular/core';
import { rssFeeds } from '../../mockdata/mock-rssFeed';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-rss-feed-overview',
  standalone: true,
  imports: [NgFor],
  templateUrl: './rss-feed-overview.component.html',
  styleUrl: './rss-feed-overview.component.scss'
})
export class RssFeedOverviewComponent {

  RssFeeds = rssFeeds;

}
