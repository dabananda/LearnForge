import { Routes } from '@angular/router';

export const AUTH_ROUTES: Routes = [
  {
    path: 'login',
    loadComponent: () =>
      import('./pages/login-page.component/login-page.component').then((m) => m.LoginPageComponent),
  },
  {
    path: 'register',
    loadComponent: () =>
      import('./pages/register-page.component/register-page.component').then((m) => m.RegisterPageComponent),
  }
];
