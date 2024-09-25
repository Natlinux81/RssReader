/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { RssServiceService } from './RssService.service';

describe('Service: RssService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RssServiceService]
    });
  });

  it('should ...', inject([RssServiceService], (service: RssServiceService) => {
    expect(service).toBeTruthy();
  }));
});
