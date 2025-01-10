import {Pipe, PipeTransform} from '@angular/core';

@Pipe({
  name: 'shortenString',
  standalone: true
})
export class ShortenStringPipe implements PipeTransform {

  transform(value: string, maxLength: number = 30): string {
    if (value.length > maxLength) {
      return value.slice(0, maxLength) + '...';
    }
    return value;
  }
}
