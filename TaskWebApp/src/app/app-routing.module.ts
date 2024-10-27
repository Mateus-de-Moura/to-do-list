import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HeaderComponent } from './components/header/header.component';
import { CreateTaskComponent } from './components/create-task/create-task.component';

const routes: Routes = [

  { path: '', component: HeaderComponent },
  { path: 'create', component: CreateTaskComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
