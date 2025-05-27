import { HttpEvent, HttpHeaders, HttpInterceptorFn, HttpResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { finalize, tap } from 'rxjs';
import { LoginConstants } from '../../Common/constants';
//import { SpinnerService } from './spinner.service';

export const authTokenInterceptor: HttpInterceptorFn = (req, next) => {

  //const spinner = inject(SpinnerService);
  const spinner = inject(NgxSpinnerService);
  spinner.show();

  var token = localStorage.getItem(LoginConstants.jwtTokenKey) as string;
  if (token == null) {
    token = '';
  }
  const customheaders = { 'Authorization': token, 'Content-Type': 'application/json' };
  const request = req.clone({ setHeaders: customheaders});

  return next(request).pipe(tap(async (event: HttpEvent<any>) => {

    if (event instanceof HttpResponse) {
      spinner.hide();
    }
  },
    (err: any) => {
      console.log(err);
      spinner.hide();
    }), finalize(() => {
      spinner.hide();
    }));
};
