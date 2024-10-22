import { Injectable, signal, effect } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DarkModeService {

  darkModeSignal = signal<string>(
    JSON.parse(window.localStorage.getItem('darkModeSignal') ?? 'null')
  );

  constructor() {
    effect(() => {
      window.localStorage.setItem('darkModeSignal', JSON.stringify(this.darkModeSignal()));
    });
  }

  updateDarkMode(){
    this.darkModeSignal.update((value) => (value === 'dark'? 'null' : 'dark'));
  }

}
