import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdPointerCompaniesComponent } from './ad-pointer-companies.component';

describe('AdPointerCompaniesComponent', () => {
  let component: AdPointerCompaniesComponent;
  let fixture: ComponentFixture<AdPointerCompaniesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdPointerCompaniesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdPointerCompaniesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
