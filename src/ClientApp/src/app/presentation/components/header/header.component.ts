import {Component, inject, SecurityContext, viewChild} from '@angular/core';
import {DarkModeService} from '../../../infrastructure/services/dark-mode.service';
import {FormsModule, NgForm} from "@angular/forms";
import {RssFeedItemRequest} from "../../models/rss-feed-item-request";
import {RssFeedRequest} from "../../models/rss-feed-request";
import {NgIf} from "@angular/common";
import {RssService} from "../../../infrastructure/services/rss.service";
import {ToastService} from "../../../infrastructure/services/toast.service";
import {DomSanitizer} from "@angular/platform-browser";
import {SanitizerService} from "../../../infrastructure/services/sanitizer.service";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-header',
  imports: [
    FormsModule,
    NgIf,
    RouterLink
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  darkModeService: DarkModeService = inject(DarkModeService);
  toastService = inject(ToastService);
  rssService = inject(RssService);
  sanitizer = inject(DomSanitizer);
  sanitizerService = inject(SanitizerService);


  feedItems: RssFeedItemRequest[] = [];
  inputRssFeed: string = "";
  channelTitle: string = '';
  private formInput = viewChild<NgForm>('formInput');

  toggleDarkMode() {
    this.darkModeService.updateDarkMode();
  }

  addFeed(): void {
    const sanitizedInput = this.sanitizer.sanitize(SecurityContext.URL, this.inputRssFeed) || '';
    const sanitizedFeedItems = this.feedItems.map(item => this.sanitizerService.sanitizeFeed(item))
    const rssFeedRequest: RssFeedRequest = {

      url: sanitizedInput,
      channelTitle: this.channelTitle,
      feedItems: sanitizedFeedItems,
    };
    this.rssService.addRssFeed(rssFeedRequest, this.inputRssFeed).subscribe({
      next: (result) => {
        console.log('Feed added successfully', result);
        this.toastService.show(result.value, {
          classname: 'bg-success text-light',
          delay: 3000
        });

    },
  error: (err) => {
      console.error('Rss-Feed already exists', err.error.error.message);
      this.toastService.show(err.error.error.message, {
        classname: 'bg-danger text-light',
        delay: 3000
      });
    }
    });
    this.formInput()!.resetForm();
  }
}
