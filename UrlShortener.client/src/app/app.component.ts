import { Component } from '@angular/core';
import { RouterOutlet, Router, RouterModule } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { MatMenuModule } from '@angular/material/menu';
import { UserInfo } from './shared/models/user-info';
import { AuthService } from './shared/services/auth.service';
import { Subscription } from 'rxjs';



@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    RouterModule,
    CommonModule,
    MatMenuModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers: [
  ]
})

export class AppComponent {
  isLoggedIn = false;
  private authSubscription: Subscription | undefined;
  userInfo: UserInfo | null = null;

  title = 'UrlShortener.client';
  constructor(private router: Router,
    private authService: AuthService) { }

  ngOnInit(): void {
    this.authSubscription = this.authService.isAuth$.subscribe(isAuth => {
      this.isLoggedIn = isAuth;
      if (this.isLoggedIn) {
        this.userInfo = this.authService.decodeToken(this.authService.getToken() as string);
      }
    });
    this.authService.isAuthenticated();
  }

  ngOnDestroy() {
    if (this.authSubscription) {
      this.authSubscription.unsubscribe();
    }
  }

  navigateToLogin() {
    this.router.navigate(['/app-login']);
  }

  navigateToHome() {
    this.router.navigate(['']);
  }

  logout(): void {
    this.authService.onLogout();
    this.userInfo = null;
  }
}
