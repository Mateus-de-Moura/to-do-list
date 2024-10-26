import { Component, OnInit, OnDestroy } from '@angular/core';
import { TaskService } from '../../services/tasks.service';
import { Config } from 'datatables.net';
import { Subject } from 'rxjs';
import { DataTablesResponse } from '../../models/datatables-response.model';
import { HttpClient, HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})

export class HeaderComponent implements OnInit, OnDestroy {

  tasks!: any;
  dtoptions: Config = {};
  dttrigger: Subject<any> = new Subject<any>();
  searchQuery: string = '';
  dtInstance: any; 

  constructor(private taskService: TaskService, private HttpClient: HttpClient) { }

  ngOnInit(): void {
    this.LoadDatabase();
    this.initDataTable();
  }

  initDataTable() {
    this.dtoptions = {
      paging: true,
      pageLength: 10,
      serverSide: true,
      searching: false,
      lengthChange: false,
      pagingType: 'full_numbers',
      ordering: false,

      ajax: (dataTablesParameters: any, callback: any) => {
        let params = new HttpParams()
          .set('pageNumber', dataTablesParameters.pageNumber)
          .set('query', this.searchQuery);

        this.HttpClient.post<DataTablesResponse>('https://localhost:7009/api/Tasks', params)
          .subscribe(resp => {
            callback({
              recordsTotal: resp.recordsTotal,
              recordsFiltered: resp.recordsFiltered,
              data: resp.data
            });
          });
      },
      columns: [
        { title: 'Completa', data: 'completed' },
        { title: 'Descrição', data: 'description' },
        { title: 'Data de Criação', data: 'createdAt' },
        { title: 'Ações', data: null },
      ]
    };

    // Recarregar a DataTable
    this.dttrigger.next(null);
  }

  LoadDatabase() {
    this.taskService.getAllTasks().subscribe(item => {
      this.tasks = item;
      this.dttrigger.next(null);
    });
  }

  onSearch() {
    const inputElement = document.getElementById('input-search') as HTMLInputElement;
    this.searchQuery = inputElement.value;

    this.ngOnDestroy()
    this.initDataTable(); 
  }

  ngOnDestroy(): void {
    
    if (this.dtInstance) {
      this.dtInstance.destroy();
    }
  }
}
