import { Component, OnInit, ViewChild } from '@angular/core';
import { GlobalConstants } from '../../../common/global-constants/global-constants';
import { SelectListItem } from '../../../shared/models/helper/select-list-item/select-list-item';
import { ChartReportModel } from '../../../shared/models/analytics/ad-pointer/chartRepords/chart-report.model';
import { Spinkit } from 'ng-http-loader';
import { Observable } from 'rxjs';
import { FormControl, FormGroup } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { AnalyticsService } from '../../analytics.service';
import { ActivatedRoute } from '@angular/router';
import { UtilsService } from '../../../common/services/utils.service';
import { first } from 'rxjs/operators';
import { SocialNetworkReachModel } from '../../../shared/models/analytics/ad-pointer/social-network-reach/social-network-reach.model';

@Component({
  selector: 'app-ad-pointer-drive-to-web',
  templateUrl: './ad-pointer-drive-to-web.component.html',
  styleUrls: ['./ad-pointer-drive-to-web.component.css']
})
export class AdPointerDriveToWebComponent implements OnInit {

  //RegionFields

  value: any;
  errorModel: any;
  apiUrl = GlobalConstants.apiAnalyticsURL;
  userId: string = '';
  searchFilterItems = Array<SelectListItem>();
  tempSearchFilterItems = Array<SelectListItem>();
  socialNetworkReachModel: SocialNetworkReachModel = new SocialNetworkReachModel;
  googleDataReachModels: any;
  facebookDataPageViewsModels:any;
  instagramDataProfileViewsModels:any;
  youtubeDataViewsModels:any;
  linkedInDataReachModels:any;
  twitterDataReachModels:any;
  spinnerStyle = Spinkit;
  //charts
  legend: boolean = true;
  showLabels: boolean = true;
  animations: boolean = true;
  xAxis: boolean = true;
  yAxis: boolean = true;
  showYAxisLabel: boolean = true;
  showXAxisLabel: boolean = true;
  xAxisLabel: string = 'Time Period';
  yAxisLabel: string = 'Page Views';
  timeline: boolean = true;

  colorScheme = {
    domain: ['#7aa3e5','#5AA454']
  };

  // controls field
  myControl = new FormControl();
  filteredOptions: Observable<string[]> | undefined;

  range = new FormGroup({
    start: new FormControl(),
    end: new FormControl()
  });
  //table field
  displayedColumns: string[] = ['name', 'value'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  //#endregion

  //#region Constructor
  constructor(private _analytycsService: AnalyticsService, private _route: ActivatedRoute, private _utilsService: UtilsService) {
    //this.initialze();

    this._route.params.subscribe(params => {
      this.userId = params['id'];
    });

  }

  ngOnInit() {

    this.dataSource.paginator = this.paginator;
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }
  //#endregion

  //#region Public Methods

  generateSocialNetworkReach(userid: string, startDate: Date, endDate: Date) {
    this.errorModel = null;
    this.socialNetworkReachModel.UserId = userid;
    this.socialNetworkReachModel.StartDate = startDate;
    this.socialNetworkReachModel.EndDate = endDate;
    var url = GlobalConstants.apiAnalyticsURL+`/api/analytics/generatesocialnetworkreach`;
    this._analytycsService.generateSocialNetworkReach(url, this.socialNetworkReachModel)
      .pipe(first())
      .subscribe({
        next: (response) => {
          this.googleDataReachModels = response.GoogleDataReachModels;
          this.facebookDataPageViewsModels = response.FacebookDataPageViewsModels;
          this.instagramDataProfileViewsModels = response.InstagramDataProfileViewsModels;
          this.youtubeDataViewsModels = response.YoutubeDataViewsModels;
          this.linkedInDataReachModels = response.LinkedInDataReachModels;
          this.twitterDataReachModels = response.TwitterDataReachModels;

        },
        error: errorResponse => {
          this.errorModel = this._utilsService.parseErrors(errorResponse);
        }
      });
  }

  //#endregion

  //#Private Methods

  private setSearchFilterItems(items: Array<SelectListItem>) {
    this.tempSearchFilterItems = items;
  }


  //#endregion

  // initialze() {
  //   this.searchFilterItems = new Array<SelectListItem>();

  // }
}

