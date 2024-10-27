import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { TaskService } from '../../services/tasks.service';

@Component({
  selector: 'app-create-task',
  templateUrl: './create-task.component.html',
  styleUrl: './create-task.component.css'
})
export class CreateTaskComponent {

  task = {
    completed: false,
    description: ''
  };

  constructor(private router: Router, private taskService: TaskService ) { }

  returnToHome() {
    this.router.navigate(['']);
  }

  saveTask(){  
     let result = this.taskService.createTask(this.task.completed, this.task.description)

     console.log(result)
  }

}
