import { RssFeed } from './../../domain/entities/rssFeed';
import { IRssFeedRepository } from "../../domain/interfaces/IRssFeedRepository";
import { IGenericService } from '../../domain/interfaces/IGenericService';


export class RssFeedRepository implements IRssFeedRepository {

  rssFeed: RssFeed[] = [];

  constructor(private rssFeedService : IGenericService<RssFeed>) {}
  getByUrlAsync(url: string): Promise<RssFeed> {
    throw new Error('Method not implemented.');
  }
  readRssFeed(url: URL): Promise<RssFeed> {
    throw new Error('Method not implemented.');
  }


}
