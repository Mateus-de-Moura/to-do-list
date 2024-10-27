import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { TaskService } from '../../services/tasks.service';
import { Config } from 'datatables.net';
import { Subject } from 'rxjs';
import { DataTablesResponse } from '../../models/datatables-response.model';
import { HttpClient, HttpParams } from '@angular/common/http';
import { DataTableDirective } from 'angular-datatables';
import { Router } from '@angular/router';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})

export class HeaderComponent implements OnInit {

  tasks!: any;
  dtoptions: Config = {};
  dttrigger: Subject<any> = new Subject<any>();
  searchQuery: string = '';
  dtInstance: any;
  @ViewChild(DataTableDirective, { static: false })
  dtElement!: DataTableDirective;

  constructor(private HttpClient: HttpClient, private router: Router) { }

  ngOnInit(): void {
    this.initDataTable();
    ;

  }

  navigateToCreate() {
    this.router.navigate(['/create']);
  }

  navigateToEdit(id: string) {    
    this.router.navigate(['/edit', id]);
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
      language: {
        processing: `<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i>`,
        emptyTable: "Sem dados disponíveis",
        lengthMenu: "Mostrar _MENU_ registros",
        zeroRecords: "Não foram encontrados resultados",
        info: "Mostrando de _START_ até _END_ de _TOTAL_ registros",
        infoEmpty: "Mostrando de 0 até 0 de 0 registros",
        infoFiltered: "",
        infoPostFix: "",
        search: "Procurar:",
        url: "",
        paginate: {
          next: '<i class="fa fa-chevron-right"></i>',
          previous: '<i class="fa fa-chevron-left"></i>',
          first: '<i class="fa fa-angle-double-left"></i>',
          last: '<i class="fa fa-angle-double-right"></i>'
        }
      },

      ajax: (dataTablesParameters: any, callback: any) => {
        let pageNumber = Math.floor(dataTablesParameters.start / dataTablesParameters.length) + 1;

        let params = new HttpParams()
          .set('pageNumber', pageNumber)
          .set('pageSize', dataTablesParameters.length.toString())
          .set('query', this.searchQuery);

        this.HttpClient.post<DataTablesResponse>('https://localhost:7009/api/Tasks', params)
          .subscribe(resp => {
            this.dttrigger.next(null);
            callback({
              recordsTotal: resp.recordsTotal,
              recordsFiltered: resp.recordsFiltered,
              data: resp.data
            });
            $('.edit-link').off('click').on('click', (event) => {
              event.preventDefault();
              const id = $(event.currentTarget).data('id');
              console.log('clicou')
              this.navigateToEdit(id);
            });
          });

      },
      columns: [
        { title: 'Completa', data: 'completed' },
        { title: 'Descrição', data: 'description' },
        { title: 'Data de Criação', data: 'createdAt' },
        {
          title: 'Ações', data: null, render: (data, type, row) => {
            return `<a class="edit-link" href="#" data-id="${row.id}">
               <i class="fa-solid fa-pen-to-square"></i></a>`;
          }
        },
      ]
    };
    this.dttrigger.next(null);
  }

  onSearch() {
    const inputElement = document.getElementById('input-search') as HTMLInputElement;
    this.searchQuery = inputElement.value;

    if (this.dtElement) {
      this.dtElement.dtInstance.then((dtInstance: any) => {
        dtInstance.ajax.reload();
      });
    }
  }
}
