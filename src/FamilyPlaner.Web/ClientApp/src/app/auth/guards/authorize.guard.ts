import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { Observable, tap } from 'rxjs';
import { AuthorizeService } from '../services/authorize.service';


@Injectable({
  providedIn: 'root',
})
export class AuthorizeGuard implements CanActivate {
  constructor(private authorize: AuthorizeService, private router: Router) {}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    return this.authorize
      .isAuthenticated()
      .pipe(
        tap(isAuthenticated => this.handleAuthorization(isAuthenticated, state))
      );
  }

  private handleAuthorization(
    isAuthenticated: boolean,
    state: RouterStateSnapshot
  ) {
    if (!isAuthenticated) {
      this.router.navigate(['/']);
    }
  }
}
