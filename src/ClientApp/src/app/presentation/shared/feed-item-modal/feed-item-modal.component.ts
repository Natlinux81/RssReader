import {Component, inject, Input} from '@angular/core';
import {RssFeedItem} from '../../../domain/entities/rssFeedItem';
import {NgClass, NgIf} from '@angular/common';
import {ShortenLinkPipe} from "../../../infrastructure/utilities/shorten-link.pipe";
import {DarkModeService} from '../../services/dark-mode.service';
import {TimeElapsedPipe} from '../../../infrastructure/utilities/time-elapsed.pipe';

@Component({
    selector: 'app-feed-item-modal',
    imports: [NgIf, ShortenLinkPipe, NgClass, TimeElapsedPipe],
    templateUrl: './feed-item-modal.component.html',
    styleUrl: './feed-item-modal.component.scss'
})
export class FeedItemModalComponent {
  darkModeService: DarkModeService = inject(DarkModeService);

  @Input() feedItem: RssFeedItem | null = null;

}
