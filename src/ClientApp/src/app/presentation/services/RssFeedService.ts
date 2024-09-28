import { RssFeed } from '../../domain/entities/rssFeed';
import { IRssFeedRepository } from "../../domain/interfaces/IRssFeedRepository";
import { IGenericRepository } from '../../domain/interfaces/IGenericRepository';


export class RssFeedService implements IRssFeedRepository {

  rssFeed: RssFeed[] = [];

  constructor(private rssFeedService : IGenericRepository<RssFeed>) {}
  getByUrlAsync(url: string): Promise<RssFeed> {
    throw new Error('Method not implemented.');
  }
  readRssFeed(url: URL): Promise<RssFeed> {
    throw new Error('Method not implemented.');
  }


}
