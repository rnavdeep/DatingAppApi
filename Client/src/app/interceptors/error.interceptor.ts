import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router:Router, private toastr:ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(

      catchError((err:HttpErrorResponse) =>{
        if(err){
            switch(err.status){
              case 400:
                this.toastr.error(err.error,err.status.toString());
                break;
              case 401:
                this.toastr.error(err.error,err.status.toString());
                break;
              case 404:
                this.router.navigateByUrl("/not-found");
                break;
              case 500:
                this.router.navigateByUrl("/server-error");
                break;
              default:
                this.toastr.error(err.error,err.status.toString());
                break;
            }
          }
          throw(err);
       }
      )
    )
  }
}
