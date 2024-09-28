/* tslint:disable:no-unused-variable */

import { TestBed,  inject } from '@angular/core/testing';
import { GenericService } from './Generic.service';

describe('Service: Generic', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GenericService]
    });
  });

  it('should ...', inject([GenericService], (service: GenericService) => {
    expect(service).toBeTruthy();
  }));
});
