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

        // Helper function - Convert database row into Todo object
        private static Todo MapReaderToTodo(SqlDataReader reader)
        {
            return new Todo
            {
                Id = reader.GetInt32("Id"),
                Title = reader.GetString("Title"),
                Description = reader.IsDBNull("Description") ? null : reader.GetString("Description"),
                IsCompleted = reader.GetBoolean("IsCompleted"),
                DueDate = reader.IsDBNull("DueDate") ? null : reader.GetDateTime("DueDate")
            };
        }

        // GET /todos
        public async Task<IEnumerable<Todo>> GetAllTodosAsync()
        {
            var todos = new List<Todo>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_GetAllTodos", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                todos.Add(MapReaderToTodo(reader));
            }

            return todos;
        }

        // POST /todos
        public async Task<int> CreateTodoAsync(Todo newTodo)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_CreateTodo", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Title", newTodo.Title);
            command.Parameters.AddWithValue("@Description", newTodo.Description ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@IsCompleted", newTodo.IsCompleted);
            command.Parameters.AddWithValue("@DueDate", newTodo.DueDate ?? (object)DBNull.Value);

            var newIdParameter = new SqlParameter("@NewId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(newIdParameter);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();


            return (int)newIdParameter.Value;
        }

        // GET /todos/{id}
        public async Task<Todo?> GetTodoByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_GetTodoByIdAsync", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return MapReaderToTodo(reader);
            }

            return null;
        }

        // PUT /todos/{id}
        public async Task<bool> UpdateTodoByIdAsync(int id, Todo updatedTodo)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_UpdateTodoById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Title", updatedTodo.Title);
            command.Parameters.AddWithValue("@Description", updatedTodo.Description ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@IsCompleted", updatedTodo.IsCompleted);
            command.Parameters.AddWithValue("@DueDate", updatedTodo.DueDate ?? (object)DBNull.Value);

            await connection.OpenAsync();
            var rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        // DELETE /todos/{id}
        public async Task<bool> DeleteTodoByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_DeleteTodoById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();
            var rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0; 
        }
    }
}
