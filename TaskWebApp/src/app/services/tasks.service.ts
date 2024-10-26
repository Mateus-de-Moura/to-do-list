import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Task } from "../models/task.model";

@Injectable({
    providedIn: 'root'
})

export class TaskService {
    
    api : string = 'https://localhost:7009/api/Tasks';

    constructor(private httpClient: HttpClient) {

    }

    getAllTasks() {
       return this.httpClient.get(this.api)
    }

    deleteTask() {

    }

    updateTask() {

    }

}