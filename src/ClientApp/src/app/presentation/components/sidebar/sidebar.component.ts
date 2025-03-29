import {Component, inject, OnInit} from '@angular/core';
import {DarkModeService} from "../../../infrastructure/services/dark-mode.service";
import {RssFeed} from "../../../domain/entities/rss-feed";
import {NgFor, NgIf} from "@angular/common";
import {RssService} from "../../../infrastructure/services/rss.service";
import {ShortenStringPipe} from "../../../infrastructure/utilities/shorten-link.pipe";

@Component({
  selector: 'app-sidebar',
  imports: [NgFor, ShortenStringPipe],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss'
})
export class SidebarComponent implements OnInit {
  darkModeService: DarkModeService = inject(DarkModeService);
  rssService = inject(RssService)

  rssFeeds: RssFeed[] = [];

  ngOnInit(): void {
    this.rssService.rssFeeds$.subscribe((feeds) => {
      this.rssFeeds = feeds;
    });

    // Initiales Laden der Feeds (falls noch nicht geladen)
    this.rssService.loadRssFeeds();
  }
}
