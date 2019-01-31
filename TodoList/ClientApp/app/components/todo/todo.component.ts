import { Component, OnInit, OnChanges } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import 'rxjs/add/operator/map';
import { TodoService } from './../../services/todo.service';
import { Todo } from "../../models/Todo";
import { OrderByPipe } from './orderby.pipe';



@Component({
    moduleId: "module.id",
    selector: 'todo',
    templateUrl: './todo.component.html'
})
export class TodoComponent implements OnInit, OnChanges {
    todos: Todo[]=[];
    model: Todo ;
    errMsg: string ;
    totalPages: number ;
    pages: any;
    currentPage: number ;
    orderProp: string;
    reverse: boolean;
   

    constructor(private _todoService: TodoService,
        private _route: ActivatedRoute, private _orderByPipe: OrderByPipe) {

    }

    ngOnInit() {
        this.todos = [];
        this.orderProp = '';
        this.reverse = false;
        this.model = {
            description: '',
            isDone: false,
            todoId: 0
        };
        

        //var id = this._route.snapshot.paramMap.get('id');
        //console.log(id);

        this._route.url.subscribe(url => {
            var id = this._route.snapshot.paramMap.get('id');
            console.log(id);
            if (id) {
                this.currentPage = Number(id)
            };
            this.orderProp = '';
            this.getTodos();

        });

    }

     getTodos() {
        this._todoService.getTodos(this.currentPage)
            .map(res => res.json())
            .subscribe(data => {
                this.totalPages = data.pages;
                this.todos = data.todoList;
                console.log('totalPages' + this.totalPages);
                this.pages = new Array(this.totalPages);
                console.log('pages' + this.pages);
            }, err => this.errMsg = err);
    }

    ngOnChanges() {
        var id = this._route.snapshot.paramMap.get('id');
        console.log(id);
    }

    add(form: NgForm) {
        if (form.invalid)
            return;

        this._todoService.add(this.model)
            .subscribe(
            data => {
                this.todos.unshift(data.json()),
                    this.model.description = ''
                form.resetForm();
                this.getTodos();
                
            },
            err => this.errMsg = err
            );
    }


    update(todo: Todo) {
        if (todo.description == '')
            return;
        this._todoService.updateTodo(todo)
            .subscribe(
            data => {
                console.log('success: ', data);
                this.getTodos();
                
            },
            err => this.errMsg = err
            );
    }
    sortTodos(orderProp: string) {
        this.orderProp = orderProp;
        this.reverse = (orderProp !== '' && this.orderProp === orderProp) ? !this.reverse : false;
       
        this.todos = this._orderByPipe.transform(this.todos, this.orderProp, this.reverse);
    }

    
    delete(todo: Todo) {
        var todos = this.todos;
        var todoId = todo.todoId;

        this._todoService.deleteTodo(todoId)
            .subscribe(
            data => {
                this.getTodos();
               
            }, err => this.errMsg = err
            );
    }

}
