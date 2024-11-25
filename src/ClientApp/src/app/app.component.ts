import {Component, inject} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {NavbarComponent} from "./presentation/components/navbar/navbar.component";
import {DarkModeService} from './infrastructure/services/dark-mode.service';
import {FooterComponent} from './presentation/components/footer/footer.component';
import {ToastComponent} from "./presentation/shared/toast/toast.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavbarComponent, FooterComponent, ToastComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'ClientApp';

  darkModeService: DarkModeService = inject(DarkModeService);

}
