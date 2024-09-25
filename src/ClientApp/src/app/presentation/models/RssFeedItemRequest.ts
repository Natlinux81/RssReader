export class RssFeedItemRequest {
  constructor(public title: string, public description: string, public link: string, public publishDate: Date, public imageUrl: string) {}
}
