import {Component, inject, ViewChild} from '@angular/core';
import { DarkModeService } from '../../services/dark-mode.service';
import {FormsModule, NgForm} from "@angular/forms";
import {RssFeedItemRequest} from "../../models/RssFeedItemRequest";
import {RssFeedRequest} from "../../models/RssFeedRequest";
import {NgIf} from "@angular/common";
import {Result} from "../../common/results/result";
import {RssService} from "../../services/rss.service";
import {RssFeedOverviewComponent} from "../rss-feed-overview/rss-feed-overview.component";
import {UpdateFeedItemsService} from "../../services/update-feed-items.service";

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    FormsModule,
    NgIf, RssFeedOverviewComponent
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent { darkModeService : DarkModeService = inject(DarkModeService);

  @ViewChild('formInput', { static: false }) formInput!: NgForm;

  feedItems: RssFeedItemRequest[] = [];
  inputRssFeed: string = "";
  channelTitle: string = '';
  constructor(private rssService: RssService, private updateRssFeedItemsService: UpdateFeedItemsService) { }

  toggleDarkMode() {
    this.darkModeService.updateDarkMode();
  }
  addFeed(): void {
    const rssFeedRequest: RssFeedRequest = {
      url: this.inputRssFeed,
      channelTitle: this.channelTitle,
      feedItems: this.feedItems
    };

    this.rssService.addRssFeed(rssFeedRequest, this.inputRssFeed).subscribe((result: Result) => {
      if (result.isSuccess) {
        this.inputRssFeed = '';
        //this.loadRssFeeds();
        this.formInput.resetForm();
        console.log('Feed added successfully', result);

      } else {
        console.error('Error adding RSS feed:', result.error);
      }
    });
  }

  updateRssFeeds() {
    this.updateRssFeedItemsService.updateFeedItems();
  }
}
