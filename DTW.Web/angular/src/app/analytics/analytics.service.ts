import { Injectable } from '@angular/core';
import { HttpService } from '../common/services/http.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { ChartReportModel } from '../shared/models/analytics/ad-pointer/chartRepords/chart-report.model';
import { GlobalConstants } from '../common/global-constants/global-constants';
import { SocialNetworkReachModel } from '../shared/models/analytics/ad-pointer/social-network-reach/social-network-reach.model';
import { NumberOfAdsByChannelModel } from '../shared/models/analytics/ad-pointer/chartRepords/number-of-ads-by-channel.model';
import { NumberOfBrandsByChannelModel } from '../shared/models/analytics/ad-pointer/chartRepords/number-of-brands-by-channel.model';
import { NumberOfCompaniesByChannelModel } from '../shared/models/analytics/ad-pointer/chartRepords/number-of-companies-by-channel.model';
import { NumberOfIndustriesByChannelModel } from '../shared/models/analytics/ad-pointer/chartRepords/number-of-industries-by-channel.model';

@Injectable()
export class AnalyticsService {


  //#region Fields

  //#endregion

  //#region Constructor
  constructor(private _httpClient: HttpClient) {
  }
  //#endregion

  //#region Public methods

  getSearchFilter(url: string) {

    return this._httpClient.get<any>(`${url}`)
      .pipe(map(resposne => {

        return resposne;
      }));
  }

  generateChartReportAds(url: string, model: ChartReportModel) {
    var test = JSON.stringify(model);

    return this._httpClient.post<any>(url, test, GlobalConstants.httpOptions)
      .pipe(map(resposne => {

        return resposne;
      }));
  }

  generateTopPercentageAds(url: string, model: ChartReportModel) {
    var test = JSON.stringify(model);

    return this._httpClient.post<any>(url, test, GlobalConstants.httpOptions)
      .pipe(map(resposne => {

        return resposne;
      }));
  }

  generateChartReportBrands(url: string, model: ChartReportModel) {
    var test = JSON.stringify(model);

    return this._httpClient.post<any>(url, test, GlobalConstants.httpOptions)
      .pipe(map(resposne => {

        return resposne;
      }));
  }

  generateChartReportCompanies(url: string, model: ChartReportModel) {
    var test = JSON.stringify(model);

    return this._httpClient.post<any>(url, test, GlobalConstants.httpOptions)
      .pipe(map(resposne => {

        return resposne;
      }));
  }

  generateChartReportIndustries(url: string, model: ChartReportModel) {
    var test = JSON.stringify(model);

    return this._httpClient.post<any>(url, test, GlobalConstants.httpOptions)
      .pipe(map(resposne => {

        return resposne;
      }));
  }
  generateNumberOfAdsByChannel(url: string, model: NumberOfAdsByChannelModel) {
    var test = JSON.stringify(model);
    return this._httpClient.post<any>(url, test, GlobalConstants.httpOptions)
      .pipe(map(resposne => {

        return resposne;
      }));
  }
  generateSocialNetworkReach(url: string, model: SocialNetworkReachModel) {
    var test = JSON.stringify(model);

    return this._httpClient.post<any>(url, test, GlobalConstants.httpOptions)
      .pipe(map(resposne => {

        return resposne;
      }));
  }
  getChannels(url: string) {
    return this._httpClient.get<any>(`${url}`)
      .pipe(map(resposne => {

        return resposne;
      }));
  }
  getSearchFilterBrands(url: string) {
    return this._httpClient.get<any>(`${url}`)
      .pipe(map(resposne => {

        return resposne;
      }));
  }
  generateNumberOfBrandsByChannel(url: string, model: NumberOfBrandsByChannelModel) {
    var test = JSON.stringify(model);
    return this._httpClient.post<any>(url, test, GlobalConstants.httpOptions)
      .pipe(map(resposne => {

        return resposne;
      }));
  }

  getSearchFilterCompanies(url: string) {
    return this._httpClient.get<any>(`${url}`)
      .pipe(map(resposne => {

        return resposne;
      }));
  }

  generateNumberOfCompaniesByChannel(url: string, model: NumberOfCompaniesByChannelModel) {
    var test = JSON.stringify(model);
    return this._httpClient.post<any>(url, test, GlobalConstants.httpOptions)
      .pipe(map(resposne => {

        return resposne;
      }));
  }
  getSearchFilterIndustries(url: string) {
    return this._httpClient.get<any>(`${url}`)
      .pipe(map(resposne => {

        return resposne;
      }));
  }
  generateNumberOfIndustriesByChannel(url: string, model: NumberOfIndustriesByChannelModel) {
    var test = JSON.stringify(model);
    return this._httpClient.post<any>(url, test, GlobalConstants.httpOptions)
      .pipe(map(resposne => {

        return resposne;
      }));
  }
  //#endregion
}
