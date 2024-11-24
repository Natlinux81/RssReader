import {Component, effect, ElementRef, inject, SecurityContext, TemplateRef, viewChild} from '@angular/core';
import {DarkModeService} from '../../services/dark-mode.service';
import {FormsModule, NgForm} from "@angular/forms";
import {RssFeedItemRequest} from "../../models/RssFeedItemRequest";
import {RssFeedRequest} from "../../models/RssFeedRequest";
import {NgIf} from "@angular/common";
import {RssService} from "../../services/rss.service";
import {ToastService} from "../../services/toast.service";
import {DomSanitizer} from "@angular/platform-browser";
import {SanitizerService} from "../../services/sanitizer.service";

@Component({
  selector: 'app-navbar',
  imports: [
    FormsModule,
    NgIf
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  darkModeService: DarkModeService = inject(DarkModeService);
  toastService = inject(ToastService);

  private formInput = viewChild <NgForm>('formInput');
  private rssFeedInput = viewChild <ElementRef<HTMLInputElement>>('rssFeedInput');
  private successTpl = viewChild <TemplateRef<any>>('successTpl');
  private dangerTpl = viewChild <TemplateRef<any>>('dangerTpl');

  feedItems: RssFeedItemRequest[] = [];
  inputRssFeed: string = "";
  channelTitle: string = '';

  constructor(private rssService: RssService,
              private sanitizer: DomSanitizer,
              private sanitizerService: SanitizerService) {
    effect(() =>{
      this.rssFeedInput()!.nativeElement.focus();
    })
  }

  toggleDarkMode() {
    this.darkModeService.updateDarkMode();
  }

  addFeed(): void {
    const sanitizedInput = this.sanitizer.sanitize(SecurityContext.URL, this.inputRssFeed) || '';
    const sanitizedFeedItems = this.feedItems.map(item => this.sanitizerService.sanitizeFeed(item))
    const rssFeedRequest: RssFeedRequest = {

      url: sanitizedInput,
      channelTitle: this.channelTitle,
      feedItems: sanitizedFeedItems,
    };
    this.rssService.addRssFeed(rssFeedRequest, this.inputRssFeed).subscribe((result) => {
      if (result.isSuccess) {
        this.formInput()!.resetForm();
        console.log('Feed added successfully', result);
        this.toastService.show({
          template: this.successTpl()!,
          classname: 'bg-success text-light',
          delay: 10000
        });
      }
    });
  }
}
