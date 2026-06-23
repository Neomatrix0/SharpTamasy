import { Component, OnInit, signal } from '@angular/core';
import { TaskItem, TaskService } from './task.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  tasks = signal<TaskItem[]>([]);

  constructor(private taskService: TaskService) {}

  ngOnInit(): void {
    this.taskService.getTasks().subscribe({
      next: tasks => {
          console.log('TASKS FROM ANGULAR:', tasks);
        this.tasks.set(tasks);
      },
      error: error => {
        console.error('Error loading tasks', error);
      }
    });
  }
}