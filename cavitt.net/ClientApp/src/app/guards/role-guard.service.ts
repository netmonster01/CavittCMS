import { AuthService } from './../services';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, Route } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable()
export class RoleGuard implements CanActivate {


  constructor(private _authService: AuthService, private _router: Router) {
  }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    const user = this._authService.decode();

    const rolesToCheck = next.data.role.split(',');

    let isInRole = false;

    if (Array.isArray(user.Role)) {

      const hasRole = user.Role.filter(function (val) {
        return rolesToCheck.indexOf(val) != -1;
      });

      if (hasRole.length >0) {
        return true;
      }
    }
    else {
      isInRole = rolesToCheck.includes(user.Role);
      if (isInRole) {
        return true;
      }
    }
    

    // navigate to not found page
    this._router.navigate(['/403']);
    return false;
  }

}
