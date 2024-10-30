import { TestBed } from '@angular/core/testing';

import { UpdateFeedItemsService } from './update-feed-items.service';

describe('UpdateFeedItemsService', () => {
  let service: UpdateFeedItemsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UpdateFeedItemsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
