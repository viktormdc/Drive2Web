import { HttpHeaders } from '@angular/common/http';
export class GlobalConstants {
  public static apiAnalyticsURL: string;
  public static httpOptions = {headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }
}
