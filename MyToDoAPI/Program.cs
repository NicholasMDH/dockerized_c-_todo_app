using MyToDoAPI.Models;
using MyToDoAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
var app = builder.Build();

// Fix HTTPS redirection in the future, it's a docker issue
// app.UseHttpsRedirection();

// ----------------------------------------
// Routes
// ----------------------------------------

//get all Todos from the database
app.MapGet("/todos", async (ITodoRepository repository) =>
{
    var todos = await repository.GetAllTodosAsync();
    return Results.Ok(todos);
});

//create a new Todo in the database
app.MapPost("/todos", async (ITodoRepository repository, Todo newTodo) =>
{
    var createdTodoId = await repository.CreateTodoAsync(newTodo);
    var createdTodo = await repository.GetTodoByIdAsync(createdTodoId);
    return Results.Created($"/todos/{createdTodoId}", createdTodo);
});

//get a specific Todo by id from the database
app.MapGet("/todos/{id}", async (ITodoRepository repository, int id) =>
{
    var returnedTodo = await repository.GetTodoByIdAsync(id);
    return returnedTodo is not null ? Results.Ok(returnedTodo) : Results.NotFound();
});

//update a specific Todo by id in the database
app.MapPut("/todos/{id}", async (ITodoRepository repository, int id, Todo updatedTodo) =>
{
    var wasUpdated = await repository.UpdateTodoByIdAsync(id, updatedTodo);
    return wasUpdated ? Results.NoContent() : Results.NotFound();
});

//delete a specific Todo by id from the database
app.MapDelete("/todos/{id}", async (ITodoRepository repository, int id) =>
{
    var wasDeleted = await repository.DeleteTodoByIdAsync(id);
    return wasDeleted ? Results.NoContent() : Results.NotFound();
});

app.Run();
