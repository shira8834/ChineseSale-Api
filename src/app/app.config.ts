import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';

// ייבוא הכלים של PrimeNG
import { providePrimeNG } from 'primeng/config';
import { definePreset } from '@primeng/themes';
import Lara from '@primeng/themes/lara';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { ConfirmationService, MessageService } from 'primeng/api';
import { authInterceptor } from './interceptor/auth-interceptor';
import { provideAnimations } from '@angular/platform-browser/animations'; // <--- חובה לייבא


export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes),
     provideHttpClient(),
     provideAnimations(),
    // , MessageService, ConfirmationService,

    providePrimeNG({
        theme: {
            preset: definePreset(Lara, {
            })
        }
    }),
    
    MessageService, 
    ConfirmationService
]
};