import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdPointerIndustriesComponent } from './ad-pointer-industries.component';

describe('AdPointerIndustriesComponent', () => {
  let component: AdPointerIndustriesComponent;
  let fixture: ComponentFixture<AdPointerIndustriesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdPointerIndustriesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdPointerIndustriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
