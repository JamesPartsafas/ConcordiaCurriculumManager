# C# Web API with PostgreSQL and .NET 7

This README provides instructions on how to set up and run the backend of the [C# Web API] project using PostgreSQL as the database and .NET 7 as the runtime environment. Please ensure that you have the necessary prerequisites installed before starting.

## Prerequisites
*Before you can run this project, make sure you have the following prerequisites installed:*

 1. **.NET 7 SDK**: You can download and install it from Microsoft's .NET
    download page.
 2. **PostgreSQL Database**: You should have PostgreSQL installed and
    running. You can download and install it from the official
    PostgreSQL website.
      3. **PostgreSQL User Credentials**: The application expects the following
    PostgreSQL user credentials:
		- *Username*: postgres
		- *Password*: password

If your PostgreSQL instance uses different credentials, you'll need to update the connection string in the project configuration in `appsettings.Development.json` accordingly.
 

## Getting Started

*To get this C# Web API project up and running, follow these steps:*

 1. **Clone the Repository**: Start by cloning the project repository to your local machine using Git.
 2. **Navigate to the Project Directory**: Change your current directory to the project's root folder.
 3. **Update Connection String**: Open the appsettings.json file in the project and update the PostgreSQL connection string *if needed*.
 4. **Build the Project**: Build the C# Web API project using the .NET CLI. *Don't forget to install the dependencies*
 5. **Apply Database Migrations**: Run the following commands to apply the database migrations: `dotnet ef database update`
 6. **Run the Application**: Start the Web API application.

Test the API: You can now access the API through a web browser  (Swagger/OpenAPI) or a tool like Postman to send HTTP requests to the endpoints defined in the project.