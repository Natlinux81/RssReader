import {Component, inject, TemplateRef, ViewChild} from '@angular/core';
import {DarkModeService} from '../../services/dark-mode.service';
import {FormsModule, NgForm} from "@angular/forms";
import {RssFeedItemRequest} from "../../models/RssFeedItemRequest";
import {RssFeedRequest} from "../../models/RssFeedRequest";
import {NgIf} from "@angular/common";
import {RssService} from "../../services/rss.service";
import {RssFeedOverviewComponent} from "../rss-feed-overview/rss-feed-overview.component";
import {UpdateFeedItemsService} from "../../services/update-feed-items.service";
import {ToastService} from "../../services/toast.service";

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
export class NavbarComponent {
  darkModeService: DarkModeService = inject(DarkModeService);
  toastService = inject(ToastService);

  @ViewChild('formInput', {static: false}) formInput!: NgForm;
  @ViewChild('successTpl', {static: false}) successTpl!: TemplateRef<any>;
  @ViewChild('dangerTpl', {static: false}) dangerTpl!: TemplateRef<any>;

  feedItems: RssFeedItemRequest[] = [];
  inputRssFeed: string = "";
  channelTitle: string = '';

  constructor(private rssService: RssService, private updateRssFeedItemsService: UpdateFeedItemsService) {
  }

  toggleDarkMode() {
    this.darkModeService.updateDarkMode();
  }

  addFeed(): void {
    const rssFeedRequest: RssFeedRequest = {
      url: this.inputRssFeed,
      channelTitle: this.channelTitle,
      feedItems: this.feedItems
    };
    this.formInput.resetForm();
    this.rssService.addRssFeed(rssFeedRequest, this.inputRssFeed).subscribe({

      next: (result) => {
        if (result.isSuccess) {
          console.log('Feed added successfully', result);
          this.toastService.show({
            template: this.successTpl,
            classname: 'bg-success text-light',
            delay: 10000
          });
        }
      },
      error: (err) => {
        console.error('Error adding RSS feed:', err);
        this.toastService.show({
          template: this.dangerTpl,
          classname: 'bg-danger text-light',
          delay: 10000
        });
      }
    });
  }

  updateRssFeeds() {
    this.updateRssFeedItemsService.updateFeedItems();
  }
}
