import { Component } from '@angular/core';
import { HeaderComponent } from './components/header/header.component';
import { HttpClient } from '@angular/common/http';
import { TaskService } from './services/tasks.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'TaskWebApp';
}
