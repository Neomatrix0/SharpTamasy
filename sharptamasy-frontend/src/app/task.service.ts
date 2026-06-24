import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export type TaskType = 'Task' | 'Bug' | 'Feature';
export type BugSeverity = 'Low' | 'Medium' | 'High';
export type FeaturePriority = 'Low' | 'Normal' | 'Urgent';

export interface TaskItem {
  id: number;
  title: string;
  description: string;
  type: string;
  status: string;
  assignedTo: string;
  severity: BugSeverity | null;
  priority: FeaturePriority | null;
}

export interface CreateTaskRequest {
  title: string;
  description: string;
  assignedTo: string;
  type: TaskType;
  severity?: BugSeverity;
  priority?: FeaturePriority;
}

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private apiUrl = 'http://localhost:5000/api/tasks';

  constructor(private http: HttpClient) {}

  getTasks(): Observable<TaskItem[]> {
    return this.http.get<TaskItem[]>(this.apiUrl);
  }

  createTask(task: CreateTaskRequest): Observable<TaskItem> {
    return this.http.post<TaskItem>(this.apiUrl, task);
  }
}
