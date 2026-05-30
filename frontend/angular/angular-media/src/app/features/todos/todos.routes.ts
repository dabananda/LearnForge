import { Routes } from '@angular/router';

export const TODOS_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./components/todos.component/todos.component').then((m) => m.TodosComponent),
  },
];
