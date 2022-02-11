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
import { NumberOfCompaniesByChannelModel } from 'src/app/shared/models/analytics/ad-pointer/chartRepords/number-of-companies-by-channel.model';

@Component({
  selector: 'app-ad-pointer-companies',
  templateUrl: './ad-pointer-companies.component.html',
  styleUrls: ['./ad-pointer-companies.component.css']
})
export class AdPointerCompaniesComponent implements OnInit {


  //#region Fields
  errorModel: any;
  apiUrl = GlobalConstants.apiAnalyticsURL;
  userId: string = '';
  searchFilterItems = Array<SelectListItem>();
  chartReportModel: ChartReportModel = new ChartReportModel;
  chartReportNumberOfCompanies:any;
  chartReportTopPercentageCompanies:any;
  numberOfCompaniesByChannel :any;
  companiesIds:any;
  channelIds:any;
  searchFilterCompaniesItems = Array<SelectListItem>();
  channelItems = Array<SelectListItem>();
  spinnerStyle = Spinkit;

  numberOfCompaniesByChannelModel:NumberOfCompaniesByChannelModel = new NumberOfCompaniesByChannelModel;

  //charts
  showXAxis: boolean = true;
  showYAxis: boolean = true;
  gradient: boolean = false;
  showLegend: boolean = false;
  showXAxisLabel: boolean = true;
  xAxisLabel: string = 'Time Period';
  showYAxisLabel: boolean = true;
  yAxisLabel: string = 'Number of Companies';
  animations: boolean = true;
  colorSchemeNumberOfCompanies = {
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
  reportNumberOfCompanies= new MatTableDataSource<any>();
  reportTopPercentageCompanies= new MatTableDataSource<any>();
  @ViewChild(MatSort, {static: false}) sort!: MatSort;
  @ViewChild('paginatorNumberOfCompanies' , {static: false})
  set paginatorNumberOfCompanies(value: MatPaginator) {
    if (this.reportNumberOfCompanies ){
      this.reportNumberOfCompanies.paginator = value;
    }
  }
  
  @ViewChild('paginatorTopPercentageCompanies' , {static: false})
  set paginatorTopPercentageCompanies(value: MatPaginator) {
    if (this.reportTopPercentageCompanies ){
      this.reportTopPercentageCompanies.paginator = value;
    }
  }
  gradient1: boolean = true;
  showLegend1: boolean = true;
  showLabels: boolean = true;
  isDoughnut: boolean = false;
  colorSchemeTopPrecentageCompanies = {
    domain: ['#5AA454', '#A10A28', '#AAAAAA', '#7FB3D5', '#F8C471', '#C39BD3'
      , '#ABEBC6', '#0B5345', '#C0392B', '#C7B42C', '#F1C40F', '#2ECC71']
  };
  legend: boolean = true;
  xAxis: boolean = true;
  yAxis: boolean = true;
  colorSchemeCompaniesByChanel = {
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
    this.reportNumberOfCompanies.paginator=this.paginatorNumberOfCompanies;
    this.reportNumberOfCompanies.sort=this.sort;
    this.reportTopPercentageCompanies.paginator=this.paginatorTopPercentageCompanies;
    this.getChannels();
  }

  ngAfterViewInit() {
    //this.reportNumberOfCompanies.paginator = this.paginatorCompanies;
  }
  //#endregion

   //#region Public Methods

   getSearchFilter(userid: string) {

    var url =GlobalConstants.apiAnalyticsURL+ `/api/analytics/getsearchfilter?userId=${userid}`;
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

  generateChartReportCompanies(userid: string, startDate: Date, endDate: Date) {
    this.errorModel = null;
    this.chartReportModel.UserId = userid;
    this.chartReportModel.StartDate = startDate;
    this.chartReportModel.EndDate = endDate;
    var url =GlobalConstants.apiAnalyticsURL+ `/api/analytics/generatenumberofcompanies`;
    this._analytycsService.generateChartReportCompanies(url, this.chartReportModel)
      .pipe(first())
      .subscribe({
        next: (response) => {

          this.chartReportNumberOfCompanies = response.ChartReportNumberOfCompaniesModels;
          this.chartReportTopPercentageCompanies = response.ChartReportTopPercentageCompaniesModels;

          this.reportNumberOfCompanies = new MatTableDataSource<any>(response.ChartReportNumberOfCompaniesModels);
          
          this.reportTopPercentageCompanies = new MatTableDataSource<any>(response.ChartReportTopPercentageCompaniesModels);
        },
        error: errorResponse => {
          this.errorModel = this._utilsService.parseErrors(errorResponse);
        }
      });
  }

  selectSearchFilter(searchFilterItem: any) {
    this.chartReportModel.SearchFilterItem = searchFilterItem;
    this.getSearchFilterCompanies(this.chartReportModel.SearchFilterItem.Key)
  }

  generateNumberOfCompaniesByChannel( startDate: Date, endDate: Date) {
    this.errorModel = null;
    this.numberOfCompaniesByChannelModel.StartDate =   startDate;
    this.numberOfCompaniesByChannelModel.EndDate = endDate;
    this.numberOfCompaniesByChannelModel.SearchChannelItems = this.channelIds;
    this.numberOfCompaniesByChannelModel.SearchCompaniesItems = this.companiesIds;
    var url =GlobalConstants.apiAnalyticsURL+ `/api/analytics/generatenumberofcompaniesbychannel`;
    this._analytycsService.generateNumberOfCompaniesByChannel(url, this.numberOfCompaniesByChannelModel)
      .pipe(first())
      .subscribe({
        next: (response) => {
          this.numberOfCompaniesByChannel = response;
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
        error: err => {

        }
      });
  }

  getSearchFilterCompanies(filterSearchId: string) {
    var url = GlobalConstants.apiAnalyticsURL+`/api/analytics/getsearchfiltercompanies?filtersearchid=${filterSearchId}`;
    this._analytycsService.getSearchFilterCompanies(url)
      .pipe(first())
      .subscribe({
        next: (response) => {
               ;
          this.searchFilterCompaniesItems = response;
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
