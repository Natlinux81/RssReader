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

  RssFeeds = rssFeeds;
}
