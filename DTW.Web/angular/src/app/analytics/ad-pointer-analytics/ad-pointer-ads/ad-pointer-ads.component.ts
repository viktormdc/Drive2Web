import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { GlobalConstants } from '../../../common/global-constants/global-constants';
import { SelectListItem } from '../../../shared/models/helper/select-list-item/select-list-item';
import { ChartReportModel } from '../../../shared/models/analytics/ad-pointer/chartRepords/chart-report.model';
import { AnalyticsService } from '../../analytics.service';
import { ActivatedRoute } from '@angular/router';
import { UtilsService } from '../../../common/services/utils.service';
import { Spinkit } from 'ng-http-loader';
import { MatPaginator,} from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { first } from 'rxjs/operators';
import { NumberOfAdsByChannelModel } from 'src/app/shared/models/analytics/ad-pointer/chartRepords/number-of-ads-by-channel.model';
import { MatSort } from '@angular/material/sort';
import { PageEvent } from '@angular/material/paginator';


@Component({
  selector: 'app-ad-pointer-ads',
  templateUrl: './ad-pointer-ads.component.html',
  styleUrls: ['./ad-pointer-ads.component.css']
})

export class AdPointerAdsComponent implements OnInit {

  //#region Fields
  value: any;
  errorModel: any;
  apiUrl = GlobalConstants.apiAnalyticsURL;
  userId: string = '';
  searchFilterItems = Array<SelectListItem>();
  searchFilterAdsItems = Array<SelectListItem>();
  channelItems = Array<SelectListItem>();
  tempSearchFilterItems = Array<SelectListItem>();
  chartReportModel: ChartReportModel = new ChartReportModel;
  chartReportNumberOfAds: any;
  chartReportTopPercentageAds: any;
  numberOfAdsByChannels :any;
  adIds:any;
  channelIds:any;
  spinnerStyle = Spinkit;
  
  

  
  numberOfAdsByChannelModel:NumberOfAdsByChannelModel = new NumberOfAdsByChannelModel;
  //charts
  showXAxis: boolean = true;
  showYAxis: boolean = true;
  gradient: boolean = false;
  showLegend: boolean = false;
  showXAxisLabel: boolean = true;
  xAxisLabel: string = 'Ad Name';
  showYAxisLabel: boolean = true;
  yAxisLabel: string = 'Number of ads';
  animations: boolean = true;
  colorSchemeNumberOfAds = {
    domain: ['#5AA454']
  };
  showLabels: boolean = true;
  isDoughnut: boolean = false;
  colorSchemeTopPrecentageAds = {
    domain: ['#5AA454', '#A10A28', '#AAAAAA', '#7FB3D5', '#F8C471', '#C39BD3'
      , '#ABEBC6', '#0B5345', '#C0392B', '#C7B42C', '#F1C40F', '#2ECC71']
  };

  legend: boolean = true;
  xAxis: boolean = true;
  yAxis: boolean = true;
  colorSchemeAdsByChanel = {
    domain: ['#5AA454', '#A10A28', '#AAAAAA', '#7FB3D5', '#F8C471', '#C39BD3'
      , '#ABEBC6', '#0B5345', '#C0392B', '#C7B42C', '#F1C40F', '#2ECC71']
  };
   xAxisLabelAdsByChanel: string = 'Time period';
   yAxisLabelAdsByChanel: string = 'Number of ads by chanel';
  timeline: boolean = true;

  // controls field
  myControl = new FormControl();
  filteredOptions: Observable<string[]> | undefined;


  range = new FormGroup({
    start: new FormControl(),
    end: new FormControl()
  });
  //table field
  displayedColumns: string[] = ['name', 'value'];
  reportNumberOfAds= new MatTableDataSource<any>();
  reportTopPercentageAds= new MatTableDataSource<any>();
  @ViewChild(MatSort, {static: false}) sort!: MatSort;
  @ViewChild('paginatorNumberOfAds', {static: false})
  set paginatorNumberOfAds(value: MatPaginator) {
    if (this.reportNumberOfAds){
      this.reportNumberOfAds.paginator = value;
    }
  }
  @ViewChild('paginatorTopPercentageAds' , {static: false})
  set paginatorTopPercentageAds(value: MatPaginator) {
    if (this.reportTopPercentageAds ){
      this.reportTopPercentageAds.paginator = value;
    }
  }
  
  //#endregion

  //#region Constructor
  constructor(private _analytycsService: AnalyticsService, private _route: ActivatedRoute, private _utilsService: UtilsService) {
    //this.initialze();

    this._route.params.subscribe(params => {
      this.userId = params['id'];
      this.getSearchFilterModels(this.userId);
    });

  }

  ngOnInit() {
    this.reportNumberOfAds.paginator = this.paginatorNumberOfAds;
    this.reportNumberOfAds.sort = this.sort;
    this.reportTopPercentageAds.paginator=this.paginatorTopPercentageAds;
    this.getChannels();
  }

  ngAfterViewInit() {
    //this.re.paginator = this.paginator;
  }
  //#endregion

  //#region Public Methods
 
 
  getSearchFilterModels(userid: string) {
         debugger;  
         console.log(GlobalConstants.apiAnalyticsURL);
    var url =GlobalConstants.apiAnalyticsURL+`/api/analytics/getsearchfilter?userId=${userid}`;
    this._analytycsService.getSearchFilter(url)
      .pipe(first())
      .subscribe({
        next: (response) => {
          this.searchFilterItems = response;
          this.setSearchFilterItems(this.searchFilterItems);
        },
        error: errorResponse => {
           this.errorModel = this._utilsService.parseErrors(errorResponse);
        }
      });
  }

  searchFilterModels(event: any): any {
    this.searchFilterItems= this.tempSearchFilterItems;

    const filterValue = event.target.value.toLowerCase();
    this.searchFilterItems = this.searchFilterItems.filter(option => option.Value.toLowerCase().includes(filterValue));
    return this.searchFilterItems;

  }

  selectSearchFilter(searchFilterItem: any) {
    this.chartReportModel.SearchFilterItem = searchFilterItem;
    this.getSearchFilterAds(this.chartReportModel.SearchFilterItem.Key)
  }

  generateChartReportAds(userid: string, startDate: Date, endDate: Date) {

    this.errorModel = null;
    this.chartReportModel.UserId = userid;
    this.chartReportModel.StartDate = startDate;
    this.chartReportModel.EndDate = endDate;
    var url = GlobalConstants.apiAnalyticsURL+ `/api/analytics/generatechartreportads`;
    this._analytycsService.generateChartReportAds(url, this.chartReportModel)
      .pipe(first())
      .subscribe({
        next: (response) => {
          this.chartReportNumberOfAds = response.ChartReportNumberOfAdsModels;
          this.chartReportTopPercentageAds = response.ChartReportTopPercentageAdsModels;
         
          this.reportNumberOfAds=new MatTableDataSource<any>(response.ChartReportNumberOfAdsModels);
          this.reportTopPercentageAds=new MatTableDataSource<any>(response.ChartReportTopPercentageAdsModels);
          
        },
        error: errorResponse => {
          this.errorModel = this._utilsService.parseErrors(errorResponse);
        }
      });
  }

  getSearchAds(userid: string) {
    var url = GlobalConstants.apiAnalyticsURL+`/api/analytics/getsearchads?userId=${userid}`;
    this._analytycsService.getSearchFilter(url)
      .pipe(first())
      .subscribe({
        next: (response) => {
          this.searchFilterItems = response;
          this.setSearchFilterItems(this.searchFilterItems);
        },
        error: errorResponse => {
          this.errorModel = this._utilsService.parseErrors(errorResponse);
        }
      });
  }

  getSearchFilterAds(filterSearchId: string) {
    var url =GlobalConstants.apiAnalyticsURL+ `/api/analytics/getsearchfilterads?filtersearchid=${filterSearchId}`;
    this._analytycsService.getSearchFilter(url)
      .pipe(first())
      .subscribe({
        next: (response) => {
               ;
          this.searchFilterAdsItems = response;
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

  generateNumberOfAdsByChannel( startDate: Date, endDate: Date) {
    this.errorModel = null;
    this.numberOfAdsByChannelModel.StartDate =   startDate;
    this.numberOfAdsByChannelModel.EndDate = endDate;
    this.numberOfAdsByChannelModel.SearchChannelItems = this.channelIds;
    this.numberOfAdsByChannelModel.SearchAdItems = this.adIds;
    var url =GlobalConstants.apiAnalyticsURL+ `/api/analytics/generatenumberofadsbychannel`;
    this._analytycsService.generateNumberOfAdsByChannel(url, this.numberOfAdsByChannelModel)
      .pipe(first())
      .subscribe({
        next: (response) => {
          this.numberOfAdsByChannels = response;
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


}

