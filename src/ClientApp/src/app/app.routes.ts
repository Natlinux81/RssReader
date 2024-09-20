import { Routes } from '@angular/router';
import { RssFeedOverviewComponent } from './components/rss-feed-overview/rss-feed-overview.component';

export const routes: Routes = [
  { path: '', redirectTo: 'rss-feed-overview', pathMatch: 'full' },
  { path: 'rss-feed-overview', component: RssFeedOverviewComponent },
];
