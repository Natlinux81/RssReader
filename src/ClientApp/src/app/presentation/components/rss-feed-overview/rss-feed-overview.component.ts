import { GenericService } from '../../../infrastructure/repositories/Generic.service';
import { Component, OnInit } from '@angular/core';
import { NgFor } from '@angular/common';
import { RssFeed} from '../../../domain/entities/rssFeed';
import { InputComponent } from "../input/input.component";
import { HttpHeaders } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-rss-feed-overview',
  standalone: true,
  imports: [NgFor, InputComponent],
  templateUrl: './rss-feed-overview.component.html',
  styleUrl: './rss-feed-overview.component.scss'
})
export class RssFeedOverviewComponent implements OnInit{
  constructor(private genericService : GenericService<RssFeed>) {}

  rssFeeds : RssFeed[] = [];

  rssFeed: RssFeed = {
    id: 3,
    url: "https://example.com/tech-rss",
    channelTitle: "Test Feed",
    feedItems: []
  }

  ngOnInit(): void {
      this.genericService.getAllAsync().subscribe((result)=>{
      this.rssFeeds = result;
      console.log("ngOnInit" , this.rssFeeds);
    })
  }

  onFeedAdded(newFeed: RssFeed) {
    this.rssFeeds.push(newFeed);  // Liste aktualisieren
    console.log("Feed added:", newFeed);
  }

  deleteFeed(rssFeed : RssFeed) : void {
    this.genericService.delete(rssFeed).subscribe();
    this.rssFeeds = this.rssFeeds.filter(r => r.id !== rssFeed.id);
    console.log("deleteFeed" , rssFeed.id);
    }
}
