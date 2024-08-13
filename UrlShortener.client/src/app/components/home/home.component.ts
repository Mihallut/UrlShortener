import { Component } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { UrlInfo } from '../../shared/models/url-info';
import { UrlShortenerService } from '../../shared/services/url-shortener.service';
import { AddUrlComponent } from '../add-url/add-url.component';
import { AuthService } from '../../shared/services/auth.service';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';



@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    MatTableModule,
    AddUrlComponent,
    CommonModule
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})

export class HomeComponent {
  displayedColumns: string[] = ['id', 'originalUrl', 'shortedUrl'];
  dataSource: UrlInfo[] = [];
  isLoggedIn: boolean = false;
  private authSubscription: Subscription | undefined;

  constructor(
    private urlService: UrlShortenerService,
    private authService: AuthService
  ) { }

  ngOnInit() {
    this.urlService.data$.subscribe(data => {
      this.dataSource = data;
    });
    this.urlService.getAll()

    this.authSubscription = this.authService.isAuth$.subscribe(isAuth => {
      this.isLoggedIn = isAuth;
    });
    this.authService.isAuthenticated();
  }
}

