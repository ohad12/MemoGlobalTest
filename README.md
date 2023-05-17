# Readme File - Memo Global Text

This readme file provides instructions on how to initialize the database for a .NET web application using Entity Framework Core (EF Core)
and the update database command.

# Database Initialization

To initialize the database for your .NET web application, follow these steps:
* Open a command prompt or terminal window.
* Navigate to the root directory of your .NET web application.
* Run the following command to apply any pending migrations and update the database schema: "dotnet ef database update"

Note: The migration file included in the project will create the ReqresUser table.

