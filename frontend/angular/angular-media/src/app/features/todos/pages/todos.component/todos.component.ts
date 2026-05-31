import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { TodosService } from '../../services/todos.service';
import { Todo } from '../../models/todo';
import { TodolistComponent } from '../../components/todolist.component/todolist.component';

@Component({
  selector: 'app-todos',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [TodolistComponent],
  templateUrl: './todos.component.html',
  styleUrl: './todos.component.css',
})
export class TodosComponent {
  private readonly todosService = inject(TodosService);
  readonly todos = signal<Todo[]>([]);
  readonly loading = signal<boolean>(true);
  readonly errors = signal<string | null>(null);

  constructor() {
    this.loadTodos();
  }

  private loadTodos() {
    this.todosService.getTodos().subscribe({
      next: (data) => {
        this.todos.set(data.splice(0, 20));
      },
      error: () => {
        this.errors.set('Failed to load todos');
      },
      complete: () => {
        this.loading.set(false);
      },
    });
  }
}
