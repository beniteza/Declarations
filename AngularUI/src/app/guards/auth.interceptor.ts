import { HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest, HttpXsrfTokenExtractor } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { tap } from "rxjs/operators";

@Injectable()
export class AuthInterceptor implements HttpInterceptor{

  constructor(private router: Router, private xsrfTokenExtractor: HttpXsrfTokenExtractor) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
      if (localStorage.getItem('token') != null) {
        // Anti forgery
        let xsrfToken = this.xsrfTokenExtractor.getToken();

        const clonedReq = req.clone({
            headers: new HttpHeaders({
                'Authorization': 'Bearer ' + localStorage.getItem('token'),
                'X-XSRF-TOKEN': xsrfToken != null ? xsrfToken : ''
              }),
            withCredentials: true
        });

        return next.handle(clonedReq).pipe(
            tap(
                (success: any) => { },
                (err: any) => {
                    if (err.status == 401){
                        localStorage.removeItem('token');
                        this.router.navigateByUrl('/login');
                    }
                }
            )
        );

      }
      else
        return next.handle(req.clone());
  }
}