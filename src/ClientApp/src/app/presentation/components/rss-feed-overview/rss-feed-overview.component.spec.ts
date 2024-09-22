import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RssFeedOverviewComponent } from './rss-feed-overview.component';

describe('RssFeedOverviewComponent', () => {
  let component: RssFeedOverviewComponent;
  let fixture: ComponentFixture<RssFeedOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RssFeedOverviewComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RssFeedOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
