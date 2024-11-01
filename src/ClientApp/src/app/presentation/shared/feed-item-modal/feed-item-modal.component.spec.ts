import {ComponentFixture, TestBed} from '@angular/core/testing';

import {FeedItemModalComponent} from './feed-item-modal.component';

describe('FeedItemModalComponent', () => {
  let component: FeedItemModalComponent;
  let fixture: ComponentFixture<FeedItemModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FeedItemModalComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(FeedItemModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
