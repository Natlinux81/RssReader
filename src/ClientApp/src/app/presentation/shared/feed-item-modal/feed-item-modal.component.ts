import { Component, Input } from '@angular/core';
import { RouterModule } from '@angular/router';
import { RssFeedItem } from '../../../domain/entities/rssFeedItem';
import { NgIf } from '@angular/common';
import { ShortenLinkPipe } from "../../../infrastructure/utilities/shorten-link.pipe";

@Component({
  selector: 'app-feed-item-modal',
  standalone: true,
  imports: [NgIf, ShortenLinkPipe],
  templateUrl: './feed-item-modal.component.html',
  styleUrl: './feed-item-modal.component.scss'
})
export class FeedItemModalComponent {
  @Input() feedItem: RssFeedItem | null = null;

}
