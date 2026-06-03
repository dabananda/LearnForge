import { computed, Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthStateService {
  readonly token = signal<string | null>(localStorage.getItem("access_token"));
  readonly isAuthenticated = computed(() => !!this.token());

  setToken(token: string) {
    localStorage.setItem("access_token", token);
    this.token.set(token);
  }

  logout() {
    this.token.set(null);
    localStorage.removeItem("access_token");
  }
}
