import { ApiService, LoginRequest } from '../../login.service';
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';       // For *ngIf and other directives
import { FormsModule } from '@angular/forms';         // For ngModel
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,             // <-- mark as standalone
  imports: [CommonModule, FormsModule],  // <-- import modules here
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  userName = '';
  password = '';
  message = '';

  constructor(private apiService: ApiService, private router: Router) {}

  onSubmit(): void {
    const credentials: LoginRequest = {
      userName: this.userName,
      password: this.password
    };

    this.apiService.login(credentials).subscribe({
    next: (response) => {
      this.message = response.message;
      if (response.message === 'Login successful') {
        this.router.navigate(['/olympic-grid']);
      }
    },
    error: (err) => {
      this.message = 'Login failed: Invalid username or password.';
      console.error(err);
    }
  });
  }
}
