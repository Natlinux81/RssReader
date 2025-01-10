import {ShortenStringPipe} from './shorten-link.pipe';

describe('ShortenLinkPipe', () => {
  it('create an instance', () => {
    const pipe = new ShortenStringPipe();
    expect(pipe).toBeTruthy();
  });
});
