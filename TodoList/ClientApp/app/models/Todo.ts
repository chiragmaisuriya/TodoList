export interface Todo {
    todoId: number,
    description: string,
    isDone: boolean,
    [key: string]: any;
}