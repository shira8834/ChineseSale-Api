import { Component, inject, PLATFORM_ID } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { CardModule } from 'primeng/card';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { UserService } from '../../../service/user.service';
import { Router, RouterLink } from '@angular/router';
import { CommonModule, isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule, 
    FormsModule, 
    ButtonModule, 
    InputTextModule, 
    PasswordModule, 
    CardModule, 
    ToastModule,
    RouterLink
  ],
  providers: [MessageService],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class Login {
  private messageService = inject(MessageService);
  private userService = inject(UserService);
  private router = inject(Router);
private platformId = inject(PLATFORM_ID);
  // אובייקט לכניסה - רק מייל וסיסמה
  loginData = {
    Email: '',
    Password: ''
  };

  onLogin() {
    if (!this.loginData.Email || !this.loginData.Password) {
      this.messageService.add({ 
        severity: 'warn', 
        summary: 'שדות חסרים', 
        detail: 'נא להזין אימייל וסיסמה' 
      });
      return;
    }

    console.log('שולח נתוני התחברות:', this.loginData);

    this.userService.LogInUser(this.loginData).subscribe({
      next: (res) => {
        if (isPlatformBrowser(this.platformId)) {
       if (res && res.token) {
      localStorage.setItem('token', res.token); 
      localStorage.setItem('user', JSON.stringify(res)); 
      this.router.navigate(['/']);
    }}
      },
     error: (err) => {
  console.error('Login Error:', err);

  if (err.status === 0) {
    this.messageService.add({ 
      severity: 'error', 
      summary: 'שגיאת תקשורת', 
      detail: 'לא ניתן להתחבר לשרת.' 
    });
  } 

  else {
    this.messageService.add({ 
      severity: 'error', 
      summary: 'כניסה נכשלה', 
      detail: 'אימייל או סיסמה שגויים.' 
    });
  }
}
    });
  }
}