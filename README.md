# Task Management Application

This is a task management backend web application built with a .NET Core , and SQL Server for data storage. The application uses Entity Framework for database interactions.

## Prerequisites

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Technologies Used
- **Backend**: .NET 6.0
- **Database**: SQL Server
- **ORM**: Entity Framework Core
- **Logging**: Microsoft.Extensions.Logging

## Setup

### 1. Clone the Repository

git clone https://github.com/omerDekel/TaskManagementApi.git
cd task-management-app
### 2. Set Up the .NET Core API
Navigate to the backend project directory:
run in cmd:
cd TaskManagementApi
### 3. Restore Packages
run in cmd:
dotnet restore
### 4. Create the Database
Execute the setupTaskDbSchema.sql script to create the database schema.

Using SQL Server Management Studio (SSMS)
Open SSMS and connect to your SQL Server.
Open a new query window.
Copy and paste the contents of setupsetupTaskDbSchema.sql into the query window.
Execute the script by clicking the "Execute" button or pressing F5.
Using SQLCMD
Alternatively, you can use sqlcmd from the command line:

sqlcmd -S your_server -U your_user -P your_password -i setupsetupTaskDbSchema.sql
### 5. Update Database Connection String
Update the connection string in appsettings.json with your SQL Server details:

json
"ConnectionStrings": {
    "TaskDb": "Server=your_server;Database=TaskDb;User Id=your_user;Password=your_password;"
}
### 6. Run the API
run in cmd:

dotnet run

### Testing
Navigate to the test project directory:

run:
cd ../TaskManagementApi.Tests

Run the tests:
dotnet test


### Project Structure
* TaskManagement.BusinessLogic - Responsible for managing the logic of task management including CRUD operations service interfaces and implementations.
* TaskManagementApi: Main API project.
  ** Controllers: API controllers to handle HTTP requests.
  ** MIddlewares: Using middleware to handle exceptions globally.
* TaskManagement.DTOs: Contains direct models for the application.
* TaskManagement.DataAccessLayer: Contains  Entity Framework database context and configuration.
  Using repository design pattern for abstract the interaction with the database and make it easier to switch to a different database system.
  Using Auto Mapper.

### API Endpoints
* GET /api/tasks: Retrieve all tasks.
* GET /api/tasks/{id}: Retrieve a task by ID.
* POST /api/tasks: Create a new task.
* PUT /api/tasks/{id}: Update an existing task.
* DELETE /api/tasks/{id}: Delete a task by ID.
