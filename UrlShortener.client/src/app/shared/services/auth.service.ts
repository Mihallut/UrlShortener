import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from "../../envireoments/environment.development";
import { BehaviorSubject, Observable, of } from "rxjs";
import { CookieService } from 'ngx-cookie-service';
import { tap } from 'rxjs/operators';
import { catchError, map } from 'rxjs/operators';
import { jwtDecode } from "jwt-decode";
import { UserInfo } from "../models/user-info";

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  loginUrl = environment.apiBaseUrl + '/User/sign-in'
  registerUrl = environment.apiBaseUrl + '/User/sign-up'
  validateUrl = environment.apiBaseUrl + '/User/validate'
  private isAuth = new BehaviorSubject<boolean>(false);
  isAuth$ = this.isAuth.asObservable();

  constructor(private http: HttpClient, private cookieService: CookieService) {

  }

  getToken(): string | null {
    return this.cookieService.get('jwt');
  }

  decodeToken(token: string): any {
    try {
      const decoded = jwtDecode(token) as any;
      const userInfo: UserInfo = {
        id: decoded.userId,
        name: decoded.userName,
        email: decoded.userEmail,
      };
      return userInfo;
    } catch (Error) {
      console.error('Invalid token', Error);
      return null;
    }
  }

  onLogin(obj: any): Observable<any> {
    return this.http.post(this.loginUrl, obj).pipe(
      tap((response: any) => {
        const token = response.token;
        this.cookieService.set('jwt', token);
        this.isAuth.next(true);
      })
    );
  }

  onRegister(obj: any): Observable<any> {
    return this.http.post(this.registerUrl, obj);
  }

  onLogout() {
    this.cookieService.delete('jwt');
    this.isAuth.next(false);
    this.isAuthenticated();
  }


  isAuthenticated() {
    const token = this.getToken();
    if (!token) {
      this.isAuth.next(false);
    }
    this.http.post<{ valid: boolean }>(this.validateUrl, {}).pipe(
      map(response => this.isAuth.next(response.valid)),
      catchError((err) => {
        this.isAuth.next(false);
        return of(false);
      })
    ).subscribe();
  }

}
