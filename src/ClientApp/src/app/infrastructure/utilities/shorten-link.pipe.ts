import {Pipe, PipeTransform} from '@angular/core';

@Pipe({
  name: 'shortenLink',
  standalone: true
})
export class ShortenLinkPipe implements PipeTransform {

  transform(value: string, maxLength: number = 30): string {
    if (value.length > maxLength) {
      return value.slice(0, maxLength) + '...';
    }
    return value;
  }
}
