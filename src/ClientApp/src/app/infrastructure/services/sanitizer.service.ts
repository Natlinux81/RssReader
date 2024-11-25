import {inject, Injectable, SecurityContext} from '@angular/core';
import {RssFeedItemRequest} from "../../presentation/models/RssFeedItemRequest";
import {DomSanitizer} from "@angular/platform-browser";

@Injectable({
  providedIn: 'root'
})
export class SanitizerService {
  sanitizer = inject(DomSanitizer)

  sanitizeFeed(feedItem: RssFeedItemRequest): RssFeedItemRequest {
    return {
      ...feedItem,
      title: this.sanitizer.sanitize(SecurityContext.HTML, feedItem.title) || '',
      description: this.sanitizer.sanitize(SecurityContext.HTML, feedItem.description) || '',
      link: this.sanitizer.sanitize(SecurityContext.HTML, feedItem.link) || '',
      publishDate: new Date(this.sanitizer.sanitize(SecurityContext.HTML, feedItem.publishDate) || ''),
      imageUrl: this.sanitizer.sanitize(SecurityContext.HTML, feedItem.imageUrl) || ''
    };
  }
}
