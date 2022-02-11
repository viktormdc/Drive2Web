import { Component } from '@angular/core';
import { TranslateService } from './shared/translate/translate.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'angular';
  constructor(private _translate:TranslateService){
    this._translate.use("en-US");
  }
}
