using MyToDoAPI.Models;
using MyToDoAPI.Repositories;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Fix HTTPS redirection in the future, it's a docker issue
// app.UseHttpsRedirection();

// Routes
app.MapGet("/todos", () =>
{
    //get all Todos from the database
});

app.MapPost("/todos", () =>
{
    //create a new Todo in the database
});

app.MapGet("/todos/{id}", (int id) =>
{
    //get a specific Todo by id from the database
});

app.MapPut("/todos/{id}", (int id, Todo updatedTodo) =>
{
    //update a specific Todo by id in the database
});

app.MapDelete("/todos/{id}", (int id) =>
{
    //delete a specific Todo by id from the database
});


app.Run();