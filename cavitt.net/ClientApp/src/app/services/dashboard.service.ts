import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Dashboard } from '../models';
import { Observable } from 'rxjs/Observable';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });

  constructor(private _http: HttpClient) { }

  getDashboard() {
    return this._http.get<Dashboard>('/api/Dashboard')
      .catch(this.handleError);
  }

  handleError(handleError: any): any {
    console.log(handleError);
  }
}
