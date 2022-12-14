import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import {BookingService} from "./booking.service";

@Injectable()
export class ReqInterceptor implements HttpInterceptor {

  constructor(private router: Router, private bookingService: BookingService) {

  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    let headers: HttpHeaders = request.headers;//.set('Authorization', `Bearer ${this.authService.accessToken}`);
    if (!request.headers.has('Content-Type')) {
      headers = headers.set('Content-Type', 'application/json');
    }
    headers = headers.set('Currency', this.bookingService.userCurrency );

    if (!request.headers.has('Accept')) {
      headers = headers.set('Accept', 'application/json');
    }

    request = request.clone({ headers });

    return next.handle(request).pipe(
      catchError(response => {
        if (response instanceof HttpErrorResponse) {
          //noop
        }

        return throwError(response);
      })
    );

  }

}
