import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { TaskService } from '../../services/tasks.service';
import Swal from 'sweetalert2';

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

  constructor(private router: Router, private taskService: TaskService) { }

  returnToHome() {
    this.router.navigate(['']);
  }

  async saveTask() {

    if (this.task.description === "" || this.task.description === undefined) {
      $('.error-description').text('campo obrigatório')
    }else{
      await this.taskService.createTask(this.task.completed, this.task.description)

      Swal.fire({
        icon: 'success',
        title: 'Sucesso!',
        text: `Cadastrado com Sucesso!`,
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 2000,
        timerProgressBar: true
      }).then(() => {
        this.router.navigate(['']);
      });
    }

  

  }

}
