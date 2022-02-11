import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './admin/login/login.component';
import { AdPointerAnalyticsComponent } from './analytics/ad-pointer-analytics/ad-pointer-analytics.component';

const routes: Routes = [

  { path: 'admin/login', component: LoginComponent },
  { path: 'analytics/ad-pointer/:id', component: AdPointerAnalyticsComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
