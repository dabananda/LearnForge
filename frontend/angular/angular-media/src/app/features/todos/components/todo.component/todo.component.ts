import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { Todo } from '../../models/todo';

@Component({
  selector: 'app-todo',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [],
  templateUrl: './todo.component.html',
  styleUrl: './todo.component.css',
})
export class TodoComponent {
  readonly todo = input.required<Todo>();
}
