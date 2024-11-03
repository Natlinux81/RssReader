import {Component, inject, TemplateRef} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {NavbarComponent} from "./presentation/components/navbar/navbar.component";
import {DarkModeService} from './presentation/services/dark-mode.service';
import {FooterComponent} from './presentation/components/footer/footer.component';
import {ToastComponent} from "./presentation/shared/toast/toast.component";
import {ToastService} from "./presentation/services/toast.service";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavbarComponent, FooterComponent, ToastComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'ClientApp';

  darkModeService: DarkModeService = inject(DarkModeService);

}
