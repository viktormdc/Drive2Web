import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdPointerBrandsComponent } from './ad-pointer-brands.component';

describe('AdPointerBrandsComponent', () => {
  let component: AdPointerBrandsComponent;
  let fixture: ComponentFixture<AdPointerBrandsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdPointerBrandsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdPointerBrandsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
