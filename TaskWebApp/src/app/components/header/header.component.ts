import { Component, NgModule, OnInit } from '@angular/core';
import { TaskService } from '../../services/tasks.service';
import { HttpClient } from '@angular/common/http';
import { Task } from '../../models/task.model';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'] 
})

export class HeaderComponent implements OnInit {
  tasks: Task[] = [];

  constructor(private taskService: TaskService) {    
  }
  ngOnInit(): void {
    this.loadTasks();
  }


  loadTasks(): void {
    this.taskService.getAllTasks().subscribe(
      (tasks: Task[]) => {
        this.tasks = tasks; 
        console.log(this.tasks); 
      },
      (error) => {
        console.error('Erro ao buscar tarefas:', error); 
      }
    );
  }
}
