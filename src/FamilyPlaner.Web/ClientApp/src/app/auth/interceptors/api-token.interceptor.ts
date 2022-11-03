import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthorizeService } from '../services/authorize.service';

@Injectable()
export class ApiTokenInterceptor implements HttpInterceptor {

  constructor(private authorizeService: AuthorizeService) {
    this.authorizeService
          .AuthStateChanged
          .subscribe(isAuthenticated =>{

          });
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    const token = this.authorizeService.getUserToken()
    if (token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
    }

    return next.handle(request);
  }
}
