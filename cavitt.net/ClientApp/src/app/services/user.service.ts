import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User, Role } from '../models';
import { ANONYMOUS_USER } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private httpHeaders: HttpHeaders;

  constructor(private _http: HttpClient) {

    this.httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
  }

  options = {
    headers: this.httpHeaders
  }

  user: User = ANONYMOUS_USER;

  getUsers() {
   
    return this._http.get('/api/Account/Users')
      .catch(this.handleError);
  }

  getUser() {

    return this._http.get('/api/Account/User')
      .catch(this.handleError);
  }

  getRoles() {
   
    return this._http.get('/api/Role/Roles')
      .catch(this.handleError);
  }

  addRole(role: Role) {

  }

  deleteRole(role: Role) {

  }

  addUser(user: User) {

  }

  deleteUser(user: User) {

  }
  //addUserToRole(userName: string, role: string) {

  //}

  handleError(handleError: any): any {
    console.log(handleError);
  }


}
