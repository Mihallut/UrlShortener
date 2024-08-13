import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { environment } from "../../envireoments/environment.development";
import { BehaviorSubject, Observable, map } from "rxjs";
import { UrlInfo } from "../models/url-info";
import { AuthService } from "./auth.service";
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from "@angular/material/snack-bar";

@Injectable({
  providedIn: 'root'
})

export class UrlShortenerService {
  reduceUrl = environment.apiBaseUrl + "/Url/reduce-url";
  getAllUrl = environment.apiBaseUrl + "/Url/get-all";
  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'top';
  private urlInfos = new BehaviorSubject<UrlInfo[]>([]);
  data$ = this.urlInfos.asObservable();

  constructor(private http: HttpClient, private authService: AuthService, private _snackBar: MatSnackBar) {

  }

  getAll() {
    this.http.get<UrlInfo[]>(this.getAllUrl).subscribe(res => {
      this.urlInfos.next(res)
    }
    );
  }

  onReduce(url: string): void {
    let params = new HttpParams().set('url', url);
    this.http.post<UrlInfo>(this.reduceUrl, {}, { params }).subscribe({
      next: res => {
        const currentData = this.urlInfos.getValue();
        this.urlInfos.next([...currentData, res]);
        this._snackBar.open('Reduce success', 'Ok', {
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
          panelClass: ['success-snackbar'],
          duration: 3000
        });
      },
      error: err => {
        this._snackBar.open('Server respond with status code ' + err.status + ". Make sure you ent4ered url ", 'Ok', {
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
          panelClass: ['error-snackbar'],
          duration: 10000
        });
        console.log(err)
      }
    });
  }
}
