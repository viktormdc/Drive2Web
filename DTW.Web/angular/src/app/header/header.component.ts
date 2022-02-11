import { Component, OnInit } from '@angular/core';
import { UtilsService } from '../common/services/utils.service';
@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  hasSystemError : boolean = false;
  constructor(private _utilsService: UtilsService) { }

  ngOnInit() {
    this._utilsService.hasSystemError.subscribe(
      (systemError) => {
        this.hasSystemError = systemError;
      });
  }

}
