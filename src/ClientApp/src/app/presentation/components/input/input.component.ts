import { RssFeed } from './../../../domain/entities/rssFeed';
import { Component, EventEmitter, Output } from '@angular/core';
import { RssFeedItem } from '../../../domain/entities/rssFeedItem';
import { GenericService } from '../../../infrastructure/repositories/Generic.service';
import { FormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-input',
  standalone: true,
  imports: [FormsModule, NgIf],
  templateUrl: './input.component.html',
  styleUrl: './input.component.scss'
})
export class InputComponent {

  @Output() feedAdded = new EventEmitter<RssFeed>();

  inputRssFeed: string = "";

  rssFeeds: RssFeed[] = [];

  // rssFeedItems: RssFeedItem[] = [
  //   {
  //     id: 1,
  //     title: "Tech News Today",
  //     link: "https://example.com/tech-news-today",
  //     description: "Latest updates in the world of technology.",
  //     publishDate: new Date("2024-09-01"),
  //     imageUrl: "pexels-markusspiske-3970330.jpg",
  //     rssFeedId: 3,
  //   },
  //   {
  //     id: 2,
  //     title: "Science Breakthroughs",
  //     link: "https://example.com/science-breakthroughs",
  //     description: "Recent discoveries in science and space exploration.",
  //     publishDate: new Date("2024-09-02"),
  //     imageUrl: "pexels-brotin-biswas-158640-518543.jpg",
  //     rssFeedId: 3,
  //   },
  // ];

  // rssFeed: RssFeed = {
  //   id: 3,
  //   url: "https://example.com/tech-rss",
  //   channelTitle: "Test Feed",
  //   feedItems: this.rssFeedItems,
  // }

  constructor(private genericService : GenericService<RssFeed>) {

  }

  addFeed() {
  //     this.genericService.addAsync(this.rssFeed).subscribe((result)=>{
  //     console.log("addFeed" , result);
  //     this.feedAdded.emit(result);
  //     this.inputRssFeed = "";
  // });
}}
