import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdPointerDriveToWebComponent } from './ad-pointer-drive-to-web.component';

describe('AdPointerDriveToWebComponent', () => {
  let component: AdPointerDriveToWebComponent;
  let fixture: ComponentFixture<AdPointerDriveToWebComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdPointerDriveToWebComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdPointerDriveToWebComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
