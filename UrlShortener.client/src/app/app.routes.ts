import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component'
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'app-login', component: LoginComponent },
  { path: 'app-registration', component: RegistrationComponent }
];
