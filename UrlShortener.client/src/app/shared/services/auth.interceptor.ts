import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service'; // Убедитесь, что путь правильный
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root',
})
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService, private cookieService: CookieService) { }

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    let token = this.cookieService.get('jwt');
    const modifiedReq = req.clone({
      headers: req.headers.set('AuthorizationJwt', "Bearer " + token)
    })
    return next.handle(modifiedReq);
  }
}
