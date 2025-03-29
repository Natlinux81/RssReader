import {Component, inject, Input} from '@angular/core';
import {RssFeedItem} from '../../../domain/entities/rss-feed-item';
import {NgClass, NgIf, NgOptimizedImage} from '@angular/common';
import {ShortenStringPipe} from "../../../infrastructure/utilities/shorten-link.pipe";
import {DarkModeService} from '../../../infrastructure/services/dark-mode.service';
import {TimeElapsedPipe} from '../../../infrastructure/utilities/time-elapsed.pipe';

@Component({
  selector: 'app-feed-item-modal',
  imports: [NgIf, ShortenStringPipe, NgClass, TimeElapsedPipe, NgOptimizedImage],
  templateUrl: './feed-item-modal.component.html',
  styleUrl: './feed-item-modal.component.scss'
})
export class FeedItemModalComponent {
  darkModeService: DarkModeService = inject(DarkModeService);

  @Input() feedItem: RssFeedItem | null = null;

}
