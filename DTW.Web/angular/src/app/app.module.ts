import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material.module';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { LoginComponent } from './admin/login/login.component';
import { HttpClientModule } from '@angular/common/http';
import { HttpService } from './common/services/http.service';
import { AuthenticationService } from './common/services/authentication.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AnalyticsService } from './analytics/analytics.service';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { TranslatePipe } from './shared/translate/translate.pipe';
import { TranslateService } from './shared/translate/translate.service';
import { TRANSLATION_PROVIDERS } from './shared/translate/translations';
import { UtilsService } from './common/services/utils.service';
import { AdPointerAdsComponent } from './analytics/ad-pointer-analytics/ad-pointer-ads/ad-pointer-ads.component';
import { NgHttpLoaderModule } from 'ng-http-loader';
import { AdPointerAnalyticsComponent } from './analytics/ad-pointer-analytics/ad-pointer-analytics.component';
import { AdPointerBrandsComponent } from './analytics/ad-pointer-analytics/ad-pointer-brands/ad-pointer-brands.component';
import { AdPointerCompaniesComponent } from './analytics/ad-pointer-analytics/ad-pointer-companies/ad-pointer-companies.component';
import { AdPointerIndustriesComponent } from './analytics/ad-pointer-analytics/ad-pointer-industries/ad-pointer-industries.component';
import { AdPointerDriveToWebComponent } from './analytics/ad-pointer-analytics/ad-pointer-drive-to-web/ad-pointer-drive-to-web.component';
import { MAT_DATE_FORMATS, MAT_DATE_LOCALE ,DateAdapter} from '@angular/material/core';
import { MomentDateAdapter, MAT_MOMENT_DATE_FORMATS  } from '@angular/material-moment-adapter';
import { MomentUtcDateAdapter } from './moment-utc-date-adapter';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HeaderComponent,
    FooterComponent,
    AdPointerAnalyticsComponent,
    TranslatePipe,
    AdPointerAdsComponent,
    AdPointerBrandsComponent,
    AdPointerCompaniesComponent,
    AdPointerIndustriesComponent,
    AdPointerDriveToWebComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    MatFormFieldModule,MatInputModule,MatSelectModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgxChartsModule,
    NgHttpLoaderModule.forRoot(),
    MatPaginatorModule,
    MatTableModule

  ],
  providers: [HttpService,AuthenticationService,AnalyticsService,TranslateService,TRANSLATION_PROVIDERS,UtilsService , {provide: MAT_DATE_LOCALE, useValue: 'en-GB'},{ provide: MAT_DATE_FORMATS, useValue: MAT_MOMENT_DATE_FORMATS },{ provide: DateAdapter, useClass: MomentUtcDateAdapter }],
  bootstrap: [AppComponent]
})
export class AppModule { }
