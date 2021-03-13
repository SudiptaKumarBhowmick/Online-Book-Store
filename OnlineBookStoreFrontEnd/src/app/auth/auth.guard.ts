import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private _router: Router) {

  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean | UrlTree {
    if (localStorage.getItem('token') != null)
      return true;
    else if (localStorage.getItem('app_token') != null)
      return true;
    else {
      this._router.navigate(['/user/login']);
      return false;
    }
  }
  
}
