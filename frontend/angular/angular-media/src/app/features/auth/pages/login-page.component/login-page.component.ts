import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/Auth/auth.service';

@Component({
  selector: 'app-login-page.component',
  imports: [],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.css',
})
export class LoginPageComponent {
  readonly form = new FormGroup({
    email: new FormControl('', {
      nonNullable: true,
      validators: [Validators.required, Validators.email],
    }),
    password: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
  });

  private readonly authService = inject(AuthService);

  onSubmit() {
    if (this.form.invalid) {
      return;
    }
    this.authService.login(this.form.getRawValue()).subscribe();
  }
}
