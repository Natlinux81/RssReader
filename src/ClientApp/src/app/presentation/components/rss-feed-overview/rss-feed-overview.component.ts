import { GenericService } from '../../../infrastructure/repositories/Generic.service';
import { Component, OnInit } from '@angular/core';
import { NgFor } from '@angular/common';
import { RssFeed} from '../../../domain/entities/rssFeed';
import { InputComponent } from "../input/input.component";
import { HttpHeaders } from '@angular/common/http';


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

  ngOnInit(): void {
      this.genericService.getAllAsync().subscribe((result)=>{
      this.rssFeeds = result;
    });
  }



  deleteFeed(rssFeed : RssFeed) : void {
    console.log("deleteFeed");
    this.genericService.delete(rssFeed).subscribe();
    this.rssFeeds.splice(this.rssFeeds.indexOf(rssFeed), 1);
    }
}
