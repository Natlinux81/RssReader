import {Component, inject} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {NavbarComponent} from "./presentation/components/navbar/navbar.component";
import {DarkModeService} from './presentation/services/dark-mode.service';
import {FooterComponent} from './presentation/components/footer/footer.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavbarComponent, FooterComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'ClientApp';

  darkModeService: DarkModeService = inject(DarkModeService);
}
