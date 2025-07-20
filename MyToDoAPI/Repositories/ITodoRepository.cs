using MyToDoAPI.Models;

namespace MyToDoAPI.Repositories
{
    public interface ITodoRepository
    {
        // GET /todos
        Task<IEnumerable<Todo>> GetAllTodosAsync();

        // POST /todos
        Task<int> CreateTodoAsync(Todo newTodo);

        // GET /todos/{id}
        Task<Todo?> GetTodoByIdAsync(int id);

        // PUT /todos/{id}
        Task<bool> UpdateTodoByIdAsync(int id, Todo updatedTodo);

        // DELETE /todos/{id}
        Task<bool> DeleteTodoByIdAsync(int id);
    }
}
