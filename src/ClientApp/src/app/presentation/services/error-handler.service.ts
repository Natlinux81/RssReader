import {Injectable} from '@angular/core';
import {throwError} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService {
  constructor() {
  }

  handleError(error: any): void {
    // Log the error, send it to a remote service, or perform other actions
    console.error('An error occurred:', error);

    // Use the rxjs throwError function
    throwError(() => new Error('Something went wrong'));
  }
}
