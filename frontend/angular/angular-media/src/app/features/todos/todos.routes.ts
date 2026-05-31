import { Routes } from '@angular/router';

export const TODOS_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./pages/todos.component/todos.component').then((m) => m.TodosComponent),
  },
];
