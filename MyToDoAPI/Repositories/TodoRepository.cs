using Microsoft.Data.SqlClient;
using System.Data;
using MyToDoAPI.Models;

namespace MyToDoAPI.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        // Get the connection string for the database
        private readonly string _connectionString;
        public TodoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException("Connection string 'DefaultConnection' not found");
        }

        // GET /todos
        public async Task<IEnumerable<Todo>> GetAllTodosAsync()
        {
            throw new NotImplementedException();
        }

        // POST /todos
        public async Task<int> CreateTodoAsync(Todo newTodo)
        {
            throw new NotImplementedException();
        }

        // GET /todos/{id}
        public async Task<Todo?> GetTodoByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        // PUT /todos/{id}
        public async Task<bool> UpdateTodoByIdAsync(int id, Todo updatedTodo)
        {
            throw new NotImplementedException();
        }

        // DELETE /todos/{id}
        public async Task<bool> DeleteTodoByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
