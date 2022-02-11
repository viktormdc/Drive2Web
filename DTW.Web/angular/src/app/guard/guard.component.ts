import { Injectable } from '@angular/core';
import { CanActivate, Router, ɵangular_packages_router_router_b } from '@angular/router';
// import { JwtHelperService } from '@auth0/angular-jwt';
// import { HeaderService } from '../header/header.service';
// import { UtilsService } from '../common/utils.service';


//const jwtHelper = new JwtHelperService();
@Injectable()
export class AuthGuard implements CanActivate {
  private accessToken:string="";
  constructor(private _router:Router) {

  }

  canActivate() {
    return true;
    // if (localStorage.getItem('access_token') && !jwtHelper.isTokenExpired(localStorage.getItem('access_token')!)) {

    //  // this._utilsService.userLogin.emit(true);
    //   return true;

    // } else {
    //         this._router.navigate(['admin']);
    //         return false;
    // }

  }
}
