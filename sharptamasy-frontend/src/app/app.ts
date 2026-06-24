import { Component, OnInit, signal } from '@angular/core';
import { CreateTaskRequest, TaskItem, TaskService } from './task.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  imports: [CommonModule, FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  tasks = signal<TaskItem[]>([]);
  showForm = signal(false);
  isSubmitting = signal(false);
  submitError = signal('');
  deletingTaskId = signal<number | null>(null);
  deleteError = signal('');
  newTask: CreateTaskRequest = this.emptyTask();

  constructor(private taskService: TaskService) {}

  ngOnInit(): void {
    this.taskService.getTasks().subscribe({
      next: tasks => {
        this.tasks.set(tasks);
      },
      error: error => {
        console.error('Error loading tasks', error);
      }
    });
  }

  submitTask(): void {
    const task: CreateTaskRequest = {
      title: this.newTask.title.trim(),
      description: this.newTask.description.trim(),
      assignedTo: this.newTask.assignedTo.trim(),
      type: this.newTask.type,
      severity: this.newTask.type === 'Bug' ? this.newTask.severity : undefined,
      priority: this.newTask.type === 'Feature' ? this.newTask.priority : undefined
    };

    if (!task.title || this.isSubmitting()) {
      return;
    }

    this.isSubmitting.set(true);
    this.submitError.set('');

    this.taskService.createTask(task).subscribe({
      next: createdTask => {
        this.tasks.update(tasks => [createdTask, ...tasks]);
        this.newTask = this.emptyTask();
        this.showForm.set(false);
        this.isSubmitting.set(false);
      },
      error: error => {
        console.error('Error creating task', error);
        this.submitError.set('Unable to create the task.');
        this.isSubmitting.set(false);
      }
    });
  }

  private emptyTask(): CreateTaskRequest {
    return {
      title: '',
      description: '',
      assignedTo: '',
      type: 'Task',
      severity: 'Low',
      priority: 'Low'
    };
  }

  deleteTask(id: number): void {
    if (this.deletingTaskId() !== null) {
      return;
    }

    this.deletingTaskId.set(id);
    this.deleteError.set('');

    this.taskService.deleteTask(id).subscribe({
      next: () => {
        this.tasks.update(tasks => tasks.filter(task => task.id !== id));
        this.deletingTaskId.set(null);
      },
      error: error => {
        console.error('Error deleting task', error);
        this.deleteError.set('Unable to delete the task.');
        this.deletingTaskId.set(null);
      }
    });
  }
}
