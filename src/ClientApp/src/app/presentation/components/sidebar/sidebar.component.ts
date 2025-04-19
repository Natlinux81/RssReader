import {Component, inject, OnInit} from '@angular/core';
import {RssFeed} from "../../../domain/entities/rss-feed";
import {NgFor} from "@angular/common";
import {RssService} from "../../../infrastructure/services/rss.service";
import {ShortenStringPipe} from "../../../infrastructure/utilities/shorten-link.pipe";

@Component({
  selector: 'app-sidebar',
  imports: [NgFor, ShortenStringPipe],
  templateUrl: './sidebar.component.html',
  standalone: true,
  styleUrl: './sidebar.component.scss'
})
export class SidebarComponent implements OnInit {
  rssService = inject(RssService)

  rssFeeds: RssFeed[] = [];

  ngOnInit(): void {
    this.rssService.rssFeeds$.subscribe((feeds) => {
      this.rssFeeds = feeds;
    });

    // Initial loading of feeds (if not yet loaded)
    this.rssService.loadRssFeeds();
  }
}
