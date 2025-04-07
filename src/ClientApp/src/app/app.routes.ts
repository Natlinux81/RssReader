import {Routes} from '@angular/router';
import {RssFeedOverviewComponent} from './presentation/components/rss-feed-overview/rss-feed-overview.component';
import {RegisterComponent} from "./presentation/authentication/register/register.component";
import {LoginComponent} from "./presentation/authentication/login/login.component";
import {AuthenticationGuard} from "./infrastructure/guards/authentication.guard";

export const routes: Routes = [
  {path: '', redirectTo: 'rss-feed-overview', pathMatch: 'full'},
  {path: 'rss-feed-overview', component: RssFeedOverviewComponent, canActivate:[AuthenticationGuard]},
  {path: 'register', component: RegisterComponent},
  {path: 'login', component: LoginComponent}
];
