import { Component, OnInit, ViewChild } from '@angular/core';
import { GlobalConstants } from 'src/app/common/global-constants/global-constants';
import { SelectListItem } from 'src/app/shared/models/helper/select-list-item/select-list-item';
import { ChartReportModel } from '../../../shared/models/analytics/ad-pointer/chartRepords/chart-report.model';
import { Spinkit } from 'ng-http-loader';
import { FormControl, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AnalyticsService } from '../../analytics.service';
import { ActivatedRoute } from '@angular/router';
import { UtilsService } from '../../../common/services/utils.service';
import { startWith, map, first } from 'rxjs/operators';
import { NumberOfIndustriesByChannelModel } from 'src/app/shared/models/analytics/ad-pointer/chartRepords/number-of-industries-by-channel.model';

@Component({
  selector: 'app-ad-pointer-industries',
  templateUrl: './ad-pointer-industries.component.html',
  styleUrls: ['./ad-pointer-industries.component.css']
})
export class AdPointerIndustriesComponent implements OnInit {


  //#region Fields
  errorModel: any;
  apiUrl = GlobalConstants.apiAnalyticsURL;
  userId: string = '';
  searchFilterItems = Array<SelectListItem>();
  chartReportModel: ChartReportModel = new ChartReportModel;
  chartReportNumberOfIndustries: any;
  chartReportTopPercentageIndustries: any;
  numberOfIndustriesByChannel :any;
  industriesIds:any;
  channelIds:any;
  searchFilterIndustriesItems = Array<SelectListItem>();
  channelItems = Array<SelectListItem>();
  spinnerStyle = Spinkit;

  numberOfIndustriesByChannelModel:NumberOfIndustriesByChannelModel = new NumberOfIndustriesByChannelModel;
  //charts
  showXAxis: boolean = true;
  showYAxis: boolean = true;
  gradient: boolean = false;
  showLegend: boolean = false;
  showXAxisLabel: boolean = true;
  xAxisLabel: string = 'Time Period';
  showYAxisLabel: boolean = true;
  yAxisLabel: string = 'Number of Industries';
  animations: boolean = true;
  colorSchemeNumberOfIndustries = {
    domain: ['#5AA454']
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
  reportNumberOfIndustries= new MatTableDataSource<any>();
  reportTopPercentageIndustries= new MatTableDataSource<any>();
  @ViewChild(MatSort, {static: false}) sort!: MatSort;
  @ViewChild('paginatorNumberOfIndustries' , {static: false})
  set paginatorNumberOfIndustries(value: MatPaginator) {
    if (this.reportNumberOfIndustries ){
      this.reportNumberOfIndustries.paginator = value;
    }
  }
  
  @ViewChild('paginatorTopPercentageIndustries' , {static: false})
  set paginatorTopPercentageIndustries(value: MatPaginator) {
    if (this.reportTopPercentageIndustries ){
      this.reportTopPercentageIndustries.paginator = value;
    }
  }
  gradient1: boolean = true;
  showLegend1: boolean = true;
  showLabels: boolean = true;
  isDoughnut: boolean = false;
  colorSchemeTopPrecentageIndustries = {
    domain: ['#5AA454', '#A10A28', '#AAAAAA', '#7FB3D5', '#F8C471', '#C39BD3'
      , '#ABEBC6', '#0B5345', '#C0392B', '#C7B42C', '#F1C40F', '#2ECC71']
  };
  legend: boolean = true;
  xAxis: boolean = true;
  yAxis: boolean = true;
  colorSchemeIndustriesByChanel = {
    domain: ['#5AA454', '#A10A28', '#AAAAAA', '#7FB3D5', '#F8C471', '#C39BD3'
      , '#ABEBC6', '#0B5345', '#C0392B', '#C7B42C', '#F1C40F', '#2ECC71']
  };
  //#region Constructor
  constructor(
    private _analytycsService: AnalyticsService,
    private _route: ActivatedRoute,
    private _utilsService: UtilsService
  ) {
    this._route.params.subscribe(params => {
      this.userId = params['id'];
      this.getSearchFilter(this.userId);
    });
  }

  ngOnInit() {
    //  Object.assign(this.multi);
    // this.filteredOptions = this.myControl.valueChanges
    //   .pipe(
    //     startWith(''),
    //     map(value => this._filter(value))
    //   );
    this.reportNumberOfIndustries.paginator=this.paginatorNumberOfIndustries;
   this.reportNumberOfIndustries.sort=this.sort;
   this.reportTopPercentageIndustries.paginator=this.paginatorTopPercentageIndustries;
    this.getChannels();
  }
  ngAfterViewInit() {
    //this.dataSource.paginator = this.paginator;
  }
  //#endregion

  //#region Public Methods

  getSearchFilter(userid: string) {

    var url = GlobalConstants.apiAnalyticsURL+`/api/analytics/getsearchfilter?userId=${userid}`;
    this._analytycsService.getSearchFilter(url)
      .pipe(first())
      .subscribe({
        next: (response) => {
          this.searchFilterItems = response;
        },
        error: errorResponse => {
          this.errorModel = this._utilsService.parseErrors(errorResponse);
        }
      });
  }

  generateChartReportIndustries(userid: string, startDate: Date, endDate: Date) {
    this.errorModel = null;
    this.chartReportModel.UserId = userid;
    this.chartReportModel.StartDate = startDate;
    this.chartReportModel.EndDate = endDate;
    var url = GlobalConstants.apiAnalyticsURL+`/api/analytics/generatenumberofindustries`;
    this._analytycsService.generateChartReportIndustries(url, this.chartReportModel)
      .pipe(first())
      .subscribe({
        next: (response) => {

          this.chartReportNumberOfIndustries = response.ChartReportNumberOfIndustriesModels;
          this.chartReportTopPercentageIndustries = response.ChartReportTopPercentageIndustriesModels;
          this.reportNumberOfIndustries = new MatTableDataSource<any>(response.ChartReportNumberOfIndustriesModels);
          
          this.reportTopPercentageIndustries = new MatTableDataSource<any>(response.ChartReportTopPercentageIndustriesModels);
        },
        error: errorResponse => {
          this.errorModel = this._utilsService.parseErrors(errorResponse);
        }
      });
  }

  selectSearchFilter(searchFilterItem: any) {
    this.chartReportModel.SearchFilterItem = searchFilterItem;
    this.getSearchFilterIndustries(this.chartReportModel.SearchFilterItem.Key)
  }

  generateNumberOfIndustriesByChannel( startDate: Date, endDate: Date) {
    this.errorModel = null;
    this.numberOfIndustriesByChannelModel.StartDate =   startDate;
    this.numberOfIndustriesByChannelModel.EndDate = endDate;
    this.numberOfIndustriesByChannelModel.SearchChannelItems = this.channelIds;
    this.numberOfIndustriesByChannelModel.SearchIndustryItems = this.industriesIds;
    var url = GlobalConstants.apiAnalyticsURL+`/api/analytics/generatenumberofindustriesbychannel`;
    this._analytycsService.generateNumberOfIndustriesByChannel(url, this.numberOfIndustriesByChannelModel)
      .pipe(first())
      .subscribe({
        next: (response) => {
          this.numberOfIndustriesByChannel = response;
        },
        error: errorResponse => {
          this.errorModel = this._utilsService.parseErrors(errorResponse);
        }
      });
  }

  getChannels() {
    var url =GlobalConstants.apiAnalyticsURL+ `/api/analytics/getchannels`;
    this._analytycsService.getChannels(url)
      .pipe(first())
      .subscribe({
        next: (response) => {
          this.channelItems = response;
        },
        error: errorResponse => {
          this.errorModel = this._utilsService.parseErrors(errorResponse);
        }
      });
  }

  getSearchFilterIndustries(filterSearchId: string) {
    var url = GlobalConstants.apiAnalyticsURL+`/api/analytics/getsearchfilterindustries?filtersearchid=${filterSearchId}`;
    this._analytycsService.getSearchFilterIndustries(url)
      .pipe(first())
      .subscribe({
        next: (response) => {
               ;
          this.searchFilterIndustriesItems = response;
        },
        error: errorResponse => {
          this.errorModel = this._utilsService.parseErrors(errorResponse);
        }
      });
  }

  //#endregion

  //#Private Methods
  private _filter(value: string): any {

    const filterValue = value.toLowerCase();

    return this.searchFilterItems.filter(option => option.Value.toLowerCase().includes(filterValue));
  }
  //#endregion


}
