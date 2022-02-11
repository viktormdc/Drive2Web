import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdPointerAdsComponent } from './ad-pointer-ads.component';

describe('AdPointerAdsComponent', () => {
  let component: AdPointerAdsComponent;
  let fixture: ComponentFixture<AdPointerAdsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdPointerAdsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdPointerAdsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
