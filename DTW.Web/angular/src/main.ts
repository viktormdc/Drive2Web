import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { GlobalConstants } from './app/common/global-constants/global-constants';
import { environment } from './environments/environment';

if (environment.production) {
  GlobalConstants.apiAnalyticsURL="http://ad-pointer.com:8801";
  enableProdMode();
}else{
  GlobalConstants.apiAnalyticsURL="http://localhost:8801";
}

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));
