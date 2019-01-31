import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { TodoComponent } from './components/todo/todo.component';
import { TodoService } from './services/todo.service';
import { OrderByPipe } from './components/todo/orderby.pipe'; 


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        TodoComponent,
        OrderByPipe
      
    ],
    providers: [
        TodoService,
        OrderByPipe
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'todo/:id', component: TodoComponent },
            { path: 'todo', redirectTo: 'todo/1', pathMatch: 'full' },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
