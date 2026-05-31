import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TokenService } from '../Token/token.service';
import { LoginRequest } from '../../models/login-request';
import { AuthResponse } from '../../models/auth-response';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly tokenService = inject(TokenService);
  private readonly loginUrl = 'https://localhost:7100/api/v1/auth/login';

  login(request: LoginRequest) {
    return this.http.post<AuthResponse>(this.loginUrl, request)
      .pipe(tap(res => this.tokenService.setToken(res.token)))
  }
}
