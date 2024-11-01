import {Injectable} from '@angular/core';
import {RssService} from "./rss.service";

@Injectable({
  providedIn: 'root'
})
export class UpdateFeedItemsService {

  constructor(private rssService: RssService) {
  }

  updateFeedItems() {
    this.rssService.updateRssFeedItems().subscribe({

      next: response => {
        if (response.isSuccess) {
          console.log('Feed items updated successfully:', response);
        } else {
          console.error('Error updating feed items:', response.error)
        }
      }
    });
  }
}
