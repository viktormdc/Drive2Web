import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable()
export class HttpService{
  constructor(private _httpClient:HttpClient) {
  }

public httpPost(url:string,model:any){
  return this._httpClient.post<any>(`${url}`, { model })
      .pipe(map(resposne => {

          return resposne;
      }));
}

public httpGet(url:string){
  return this._httpClient.get<any>(url)
      .pipe(map(resposne => {

          return resposne;
      }));
}

}
