-- Create the TodoDb database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'TodoDb')
BEGIN
    CREATE DATABASE TodoDb;
END
GO

-- use the TodoDb database
USE TodoDb;
GO

-- Create the Todos table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Todos')
BEGIN
    CREATE TABLE Todos (
        Id int IDENTITY(1,1) PRIMARY KEY,
        Title nvarchar(255) NOT NULL,
        Description nvarchar(1000) NULL,
        IsCompleted bit NOT NULL DEFAULT 0,
        DueDate DATETIME2 NULL
    );
END
GO

-- Insert initial data into the Todos table
IF NOT EXISTS (SELECT * FROM Todos)
BEGIN
    INSERT INTO Todos (Title, Description, IsCompleted, DueDate)
    VALUES 
        ('Learn C#', 'Complete the C# fundamentals course', 0, '2024-02-15'),
        ('Build Todo API', 'Create a REST API for Todo management', 0, '2024-02-20'),
        ('Setup Docker', 'Configure Docker containers for the application', 1, NULL);
END
GO

-- =====================================================
-- STORED PROCEDURES
-- =====================================================

-- GET /todos - Get all todos
CREATE OR ALTER PROCEDURE sp_GetAllTodos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, Title, Description, IsCompleted, DueDate
    FROM Todos ORDER BY Id;
END
GO

-- POST /todos - Create a new todo
CREATE OR ALTER PROCEDURE sp_CreateTodo
    @Title NVARCHAR(255),
    @Description NVARCHAR(1000) = NULL,
    @IsCompleted BIT,
    @DueDate DATETIME2 = NULL,
    @NewId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Todos (Title, Description, IsCompleted, DueDate)
    VALUES (@Title, @Description, @IsCompleted, @DueDate);

    SET @NewId = SCOPE_IDENTITY();
END
GO

-- GET /Todos/{id} - Get a todo by Id
CREATE OR ALTER PROCEDURE sp_GetTodoById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, Title, Description, IsCompleted, DueDate
    FROM Todos
    WHERE Id = @Id;
END
GO

-- PUT /todos/{id} - Update a todo by Id
CREATE OR ALTER PROCEDURE sp_UpdateTodoById
    @Id INT ,
    @Title NVARCHAR(255),
    @Description NVARCHAR(1000) = NULL,
    @IsCompleted BIT,
    @DueDate DATETIME2 = NULL,
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Todos
    SET Title = @Title,
        Description = @Description,
        IsCompleted = @IsCompleted,
        DueDate = @DueDate
    WHERE Id = @Id;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO

-- DELETE /todos/{id}
CREATE OR ALTER PROCEDURE sp_DeleteTodoById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Todos
    WHERE Id = @Id;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO
