
# Prerequisites
- On Mac you can install (to start a server) https://postgresapp.com/
- On Mac you can install (to connect and explore the server) https://www.pgadmin.org/download/ 
- Install dotnet entity framework globally `dotnet tool install --global dotnet-ef`

# Run Project from Command line
Inside project directory run (where the .sln-file is located)...
- `cd TaskManager`
- `dotnet run`
# Working with Migrations
Migration files are located inside /Migrations directory. To create a migration run `dotnet ef migrations add <ANY_NAME>`. To see the migration as SQL run `dotnet ef migrations script`. This is especially useful to understand if data model has been configured properly.
## Development tip
If application has never been deployed and still in development you can always manually delete the contents of the /Migrations directory and run `dotnet ef migrations add Initial` to (re)create the initial migration script. In case the previous migration script has been already applied to your local database you have to drop the database as well.
## Apply scripts to the database
So far the migration scripts are created but not applied to your database.
To apply the migration scripts to your database run `dotnet ef database update`.
## Help
`dotnet ef migrations -h`<br>
`dotnet ef database -h`
# Working with Postgreql on CLI
## Show all databases
`SELECT datname FROM pg_database;`
## Show all tables
`SELECT * FROM pg_catalog.pg_tables WHERE schemaname != 'pg_catalog' AND schemaname != 'information_schema';`
dotnet r