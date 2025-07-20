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