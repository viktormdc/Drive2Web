import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { GlobalConstants } from 'src/app/common/global-constants/global-constants';
import { SelectListItem } from 'src/app/shared/models/helper/select-list-item/select-list-item';
import { ChartReportModel } from '../../../shared/models/analytics/ad-pointer/chartRepords/chart-report.model';
import { Spinkit } from 'ng-http-loader';
import { FormControl, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { AnalyticsService } from '../../analytics.service';
import { ActivatedRoute } from '@angular/router';
import { UtilsService } from '../../../common/services/utils.service';
import { startWith, map, first } from 'rxjs/operators';
import { NumberOfBrandsByChannelModel } from 'src/app/shared/models/analytics/ad-pointer/chartRepords/number-of-brands-by-channel.model';
@Component({
  selector: 'app-ad-pointer-brands',
  templateUrl: './ad-pointer-brands.component.html',
  styleUrls: ['./ad-pointer-brands.component.css']
})
export class AdPointerBrandsComponent implements OnInit {

  //#region Fields
  errorModel: any;
  apiUrl = GlobalConstants.apiAnalyticsURL;
  userId: string = '';
  searchFilterItems = Array<SelectListItem>();
  chartReportModel: ChartReportModel = new ChartReportModel;
  chartReportNumberOfBrands: any;
  chartReportTopPercentageBrands: any;
  numberOfBrandsByChannel :any;
  brandIds:any;
  channelIds:any;
  searchFilterBrandsItems = Array<SelectListItem>();
  channelItems = Array<SelectListItem>();
  spinnerStyle = Spinkit;

  numberOfBrandsByChannelModel:NumberOfBrandsByChannelModel = new NumberOfBrandsByChannelModel;
  //charts
  showXAxis: boolean = true;
  showYAxis: boolean = true;
  gradient: boolean = false;
  showLegend: boolean = false;
  showXAxisLabel: boolean = true;
  xAxisLabel: string = 'Time Period';
  showYAxisLabel: boolean = true;
  yAxisLabel: string = 'Number of brands';
  animations: boolean = true;
  colorSchemeNumberOfBrands = {
    domain: ['#5AA454']
  };
  // controls field
  myControlBrand = new FormControl();
  filteredOptions: Observable<string[]> | undefined;
  range = new FormGroup({
    start: new FormControl(),
    end: new FormControl()
  });

  //table field
 
  displayedColumns: string[] = ['name', 'value'];
  reportNumberOfBrands= new MatTableDataSource<any>();
  reportTopPercentageBrands= new MatTableDataSource<any>();
  @ViewChild(MatSort, {static: false}) sort!: MatSort;
  @ViewChild('paginatorNumberOfBrands' , {static: false})
  set paginatorNumberOfBrands(value: MatPaginator) {
    if (this.reportNumberOfBrands ){
      this.reportNumberOfBrands.paginator = value;
    }
  }
  
  @ViewChild('paginatorTopPercetageBrands' , {static: false})
  set paginatorTopPercetageBrands(value: MatPaginator) {
    if (this.reportTopPercentageBrands ){
      this.reportTopPercentageBrands.paginator = value;
    }
  }
  
  gradient1: boolean = true;
  showLegend1: boolean = true;
  showLabels: boolean = true;
  isDoughnut: boolean = false;
  colorSchemeTopPrecentageBrands = {
    domain: ['#5AA454', '#A10A28', '#AAAAAA', '#7FB3D5', '#F8C471', '#C39BD3'
      , '#ABEBC6', '#0B5345', '#C0392B', '#C7B42C', '#F1C40F', '#2ECC71']
  };
  legend: boolean = true;
  xAxis: boolean = true;
  yAxis: boolean = true;
  colorSchemeBrandsByChanel = {
    domain: ['#5AA454', '#A10A28', '#AAAAAA', '#7FB3D5', '#F8C471', '#C39BD3'
      , '#ABEBC6', '#0B5345', '#C0392B', '#C7B42C', '#F1C40F', '#2ECC71']
  };
  app: any;
  //#region Constructor

  constructor(
    private _analytycsService: AnalyticsService,
    private _route: ActivatedRoute,
    private _utilsService: UtilsService) {
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
    
   this.reportNumberOfBrands.paginator=this.paginatorNumberOfBrands;
   this.reportNumberOfBrands.sort=this.sort;
   this.reportTopPercentageBrands.paginator=this.paginatorTopPercetageBrands;
    this.getChannels();
  }

  ngAfterViewInit() {
    //this.dataSource.paginator=this.paginator;
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

  generateChartReportBrands(userid: string, startDate: Date, endDate: Date) {
    this.errorModel = null;
    this.chartReportModel.UserId = userid;
    this.chartReportModel.StartDate = startDate;
    this.chartReportModel.EndDate = endDate;
    var url = GlobalConstants.apiAnalyticsURL+`/api/analytics/generatenumberofbrands`;
    this._analytycsService.generateChartReportBrands(url, this.chartReportModel)
      .pipe(first())
      .subscribe({
        next: (response) => {

          this.chartReportNumberOfBrands = response.ChartReportNumberOfBrandsModels;
          this.chartReportTopPercentageBrands = response.ChartReportTopPercentageBrandsModels;
          
          
          this.reportNumberOfBrands = new MatTableDataSource<any>(response.ChartReportNumberOfBrandsModels);
          
          this.reportTopPercentageBrands = new MatTableDataSource<any>(response.ChartReportTopPercentageBrandsModels);
          
        },
        error: errorResponse => {
          this.errorModel = this._utilsService.parseErrors(errorResponse);
        }
      });
  }

  selectSearchFilter(searchFilterItem: any) {
    this.chartReportModel.SearchFilterItem = searchFilterItem;
    this.getSearchFilterBrands(this.chartReportModel.SearchFilterItem.Key)
  }

  generateNumberOfBrandsByChannel( startDate: Date, endDate: Date) {
    this.errorModel = null;
    this.numberOfBrandsByChannelModel.StartDate =   startDate;
    this.numberOfBrandsByChannelModel.EndDate = endDate;
    this.numberOfBrandsByChannelModel.SearchChannelItems = this.channelIds;
    this.numberOfBrandsByChannelModel.SearchBrandlItems = this.brandIds;
    var url =GlobalConstants.apiAnalyticsURL+ `/api/analytics/generatenumberofbrandsbychannel`;
    this._analytycsService.generateNumberOfBrandsByChannel(url, this.numberOfBrandsByChannelModel)
      .pipe(first())
      .subscribe({
        next: (response) => {
          this.numberOfBrandsByChannel = response;
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
  getSearchFilterBrands(filterSearchId: string) {
    var url = GlobalConstants.apiAnalyticsURL+`/api/analytics/getsearchfilterbrands?filtersearchid=${filterSearchId}`;
    this._analytycsService.getSearchFilterBrands(url)
      .pipe(first())
      .subscribe({
        next: (response) => {
               ;
          this.searchFilterBrandsItems = response;
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

export interface PeriodicElement {
  name: string;
  value: number;
}

 const ELEMENT_DATA: PeriodicElement[] = [
  { name: 'Hydrogen', value: 1.0079, },
   { name: 'Helium', value: 4.0026 },
   { name: 'Lithium', value: 6.941 },
   { name: 'Beryllium', value: 9.0122 },
   { name: 'Boron', value: 10.811 },

 ]
