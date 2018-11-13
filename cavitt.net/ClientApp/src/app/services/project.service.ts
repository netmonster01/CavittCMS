import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {  Project } from '../models';
import { Observable } from 'rxjs/Observable';
import { LoggerService } from './logger.service';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private _http: HttpClient, private loggerService: LoggerService) { }
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });

  getProjects() {

    return this._http.get<Project[]>('/api/Projects')
      .catch(this.handleError);
  }

  createPost(project: Project) {

    let options = {
      headers: this.headers
    };

    return this._http.post<Project>('/api/Projects', project, options).catch(this.handleError);
  }

  handleError(handleError: any): any {
    console.log(handleError);
  }
}
