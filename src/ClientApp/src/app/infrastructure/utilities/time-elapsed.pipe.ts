import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'timeElapsed',
  pure: false,
  standalone: true
})
export class TimeElapsedPipe implements PipeTransform {

  transform(value: Date | string): string {
    const givenDate = new Date(value);  // date to be formatted
    const now = new Date();             // actual date
    const diffMs = now.getTime() - givenDate.getTime();  // difference in milliseconds

    const seconds = Math.floor(diffMs / 1000);
    const minutes = Math.floor(seconds / 60);
    const hours = Math.floor(minutes / 60);
    const days = Math.floor(hours / 24);

    if (days > 0) {
      return `${days} days`;
    } else if (hours > 0) {
      return `${hours} hours`;
    } else if (minutes > 0) {
      return `${minutes} minutes`;
    } else {
      return `${seconds} seconds`;
    }
  }
}
