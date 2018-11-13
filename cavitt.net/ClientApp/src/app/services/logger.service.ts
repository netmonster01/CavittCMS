import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Log } from '../models'
import { Observable } from 'rxjs/Observable';

@Injectable({
  providedIn: 'root'
})
export class LoggerService {

  constructor(private http: HttpClient) { }
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
  log: Log

  addLog(log: Log): void {

    let options = {
      headers: this.headers
    };
    console.log('log:' + log);

    this.http.post('/api/Logs', log).subscribe();

  }

  getLogs() {
    return this.http.get<Log[]>('/api/Logs').catch(this.handleError).shareReplay().do(log => console.log(log)); 
  }

  getErrorLogCount() {

    return this.http.get<number>('/api/Logs/ErrorLogCount').catch(this.handleError);
  }

  handleError(handleError: any): any {
    console.log(handleError);
  }
}
