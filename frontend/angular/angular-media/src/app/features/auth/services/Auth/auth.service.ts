import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TokenService } from '../Token/token.service';
import { LoginRequest } from '../../models/login-request';
import { tap } from 'rxjs';
import { ApiResponse } from '../../../../shared/models/api-response.model';
import { AuthStateService } from './auth-state.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly authStateService = inject(AuthStateService);
  private readonly loginUrl = 'https://localhost:7100/api/v1/users/login';

  login(request: LoginRequest) {
    return this.http
      .post<ApiResponse<string>>(this.loginUrl, request)
      .pipe(tap((res) => this.authStateService.setToken(<string>res.data)));
  }
}
