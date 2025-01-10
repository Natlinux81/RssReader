import {Component, inject} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {HeaderComponent} from "./presentation/components/header/header.component";
import {DarkModeService} from './infrastructure/services/dark-mode.service';
import {FooterComponent} from './presentation/components/footer/footer.component';
import {ToastComponent} from "./presentation/shared/toast/toast.component";
import {SidebarComponent} from "./presentation/components/sidebar/sidebar.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent, SidebarComponent, FooterComponent, ToastComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'ClientApp';

  darkModeService: DarkModeService = inject(DarkModeService);

}
