import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { Todo } from '../../models/todo';
import { TodosComponent } from '../../pages/todos.component/todos.component';
import { TodoComponent } from '../todo.component/todo.component';

@Component({
  selector: 'app-todolist',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [TodoComponent],
  templateUrl: './todolist.component.html',
  styleUrl: './todolist.component.css',
})
export class TodolistComponent {
  readonly todos = input.required<Todo[]>();
}
