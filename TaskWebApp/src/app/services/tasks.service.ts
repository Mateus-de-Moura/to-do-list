import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Task } from "../models/task.model";

@Injectable({
    providedIn: 'root'
})

export class TaskService {

    api: string = 'https://localhost:7009/api/Tasks';

    constructor(private httpClient: HttpClient) {

    }

    getAllTasks() {
        return this.httpClient.get(this.api)
    }

    deleteTask(id: string): Promise<any> {
       return this.httpClient.delete(`${this.api}/Delete?Id=${id}`).toPromise()
    }

    async edit(id: string): Promise<any> {
        return this.httpClient.get<any>(`${this.api}/Edit?Id=${id}`).toPromise();
      }  

   async createTask(completed: boolean, description: string) {
        const task = {
            Completed: completed,
            description: description,
            CreatedAt: new Date()
        }

        this.httpClient.post(`${this.api}/Create`, task)
            .subscribe(response => {
                return response;
            }, error => {        
                console.error('Error creating task', error);
            });
    }

    async updateTask(task: Task){

        this.httpClient.put(`${this.api}/Update`, task)
        .subscribe(response => {
            return response;
        }, error => {        
            console.error('Error creating task', error);
        });


    }

}