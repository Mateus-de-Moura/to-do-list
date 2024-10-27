import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TaskService } from '../../services/tasks.service';
import Swal from 'sweetalert2';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-update-task',
  templateUrl: './update-task.component.html',
  styleUrl: './update-task.component.css'
})
export class UpdateTaskComponent implements OnInit {
  id: string | null = null;

  task = {
    completed: false,
    description: '',
    Id: ''
  };

  constructor(private router: Router, private taskService: TaskService, private route: ActivatedRoute) { }


  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.id = params.get('id');

      this.getTask(this.id!);

    });
  }


 async getTask(id: string) {
     let task = await this.taskService.edit(id);

     this.task.Id = task.id;
     this.task.description = task.description;
     this.task.completed = task.completed; 
  }

  updateTask() {

    if (this.task.description === "" || this.task.description === undefined) {
      $('.error-description').text('campo obrigatório')
    }
    else {
      Swal.fire({
        icon: 'success',
        title: 'Sucesso!',
        text: `Atualizado com Sucesso!`,
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

  returnToHome() {
    this.router.navigate(['']);
  }

}
