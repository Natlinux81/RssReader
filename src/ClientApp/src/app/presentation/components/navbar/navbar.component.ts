import { Component, inject } from '@angular/core';
import { DarkModeService } from '../../services/dark-mode.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent { darkModeService : DarkModeService = inject(DarkModeService);

  constructor() { }

  toggleDarkMode() {
    this.darkModeService.updateDarkMode();
  }
}
