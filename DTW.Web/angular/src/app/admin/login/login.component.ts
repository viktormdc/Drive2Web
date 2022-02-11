import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { AuthenticationService } from '../../common/services/authentication.service';
import { LoginModel } from '../../shared/models/admin/login/login.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public loginModel: LoginModel = new LoginModel;

  constructor(private _authenticationService: AuthenticationService) { }

  ngOnInit(): void {
  }

  login(model: LoginModel) {
    this._authenticationService.login(model.UserName, model.Password)
      .pipe(first())
      .subscribe({
        next: () => {


        },
        error: err => {

        }
      });
  }

  initialize() {
  }

}
