function ToursResource($resource) {

    var URL = Constants.URL + 'api/airports/:id';

    return $resource(Constants.URL + 'api/airports/:id',
        {id: "@id", },
        {
            getAirports: { method: "GET", params: {} },
            getAirport: { method: "GET", params: { id: 6 } },
            CreateTodo: { method: "POST", params: { content: "", order: 0, done: false } },
            UpdateTodo: { method: "PATCH", params: { /*...*/ } },
            DeleteTodo: { method: "DELETE", params: { id: 0 } },
            ResetTodos: { method: "GET", params: { cmd: "reset" } },
        });

    //Usage:

    //GET without ID
    //it calls -> api/1/todo
    src.ListTodos();

    //GET with ID
    //it calls -> api/1/todo/4
    src.GetTodo({ id: 4 });

    //POST with content, order, done
    //it calls -> api/1/todo
    src.CreateTodo({ content: "learn Javascript", order: 1, done: false });

    //UPDATE content only
    //it calls -> api/1/todo/5
    src.UpdateTodo({ id: 5, content: "learn AngularJS" });

    //UPDATE done only
    //it calls -> api/1/todo/5
    src.UpdateTodo({ id: 5, done: true });

    //RESET with cmd
    //it calls -> api/1/todo/reset
    src.ResetTodos();
}