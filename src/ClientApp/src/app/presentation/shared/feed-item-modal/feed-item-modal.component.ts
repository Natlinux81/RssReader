import { Component, Input } from '@angular/core';
import { RouterModule } from '@angular/router';
import { RssFeedItem } from '../../../domain/entities/rssFeedItem';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-feed-item-modal',
  standalone: true,
  imports: [NgIf],
  templateUrl: './feed-item-modal.component.html',
  styleUrl: './feed-item-modal.component.scss'
})
export class FeedItemModalComponent {
  @Input() feedItem: RssFeedItem | null = null;

}
