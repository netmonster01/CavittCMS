import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
// import { Stats } from '../models/stats';
import { Observable } from 'rxjs/Observable';

@Injectable({
  providedIn: 'root'
})
export class StatsService {

  constructor(private http: HttpClient) { }

  stats = [];

  getStats(){
    const httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
    let options = {
      headers: httpHeaders
    };
    return this.http.get('/api/SpeedTestResults')
      .catch(this.handleError); 
  }

  handleError(handleError: any): any {
    console.log(handleError);
  }
 
}
