import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AuthService } from '../../shared/services/auth.service';
import { TokenDto } from '../../shared/models/token-responce';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { Router } from '@angular/router';


@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    ReactiveFormsModule,
    FormsModule,
  ],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})
export class RegistrationComponent {
  registrationForm = this.formBuilder.group({
    Email: [''],
    Password: [''],
    Name: ['']
  })

  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private _snackBar: MatSnackBar,
    private router: Router
  ) {

  }

  onRegister() {

    this.authService.onRegister(this.registrationForm.value).subscribe(
      {
        next: res => {
          this.authService.onLogin(this.registrationForm.value).subscribe({
            next: res => {
              this._snackBar.open('Registration  success', 'Ok', {
                horizontalPosition: this.horizontalPosition,
                verticalPosition: this.verticalPosition,
                panelClass: ['success-snackbar'],
                duration: 3000
              });
              this.router.navigate(['']);
            },
            error: err => {
              this._snackBar.open('Server respond with status code ' + err.status + ". Make sure you fill in all fields ", 'Ok', {
                horizontalPosition: this.horizontalPosition,
                verticalPosition: this.verticalPosition,
                panelClass: ['error-snackbar'],
                duration: 10000
              });
              console.log(err)
            }
          });
        }, error: err => {
          this._snackBar.open('Server respond with status code ' + err.status + ". Make sure you fill in all fields ", 'Ok', {
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition,
            panelClass: ['error-snackbar'],
            duration: 10000
          });
          console.log(err)
        }
      })
  }
}
