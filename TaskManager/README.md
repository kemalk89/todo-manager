# Prerequisites
On Mac you can install https://postgresapp.com/

# Run Project from Command line
Inside project directory run (where the .sln-file is located)...
- `cd TaskManager`
- `dotnet run`
# Working with Postgreql on CLI
### Help
`dotnet ef migrations -h`<br>
`dotnet ef database -h`
### Update datamodel
`dotnet ef database update`
### Show all databases
`SELECT datname FROM pg_database;`
### Show all tables
`SELECT * FROM pg_catalog.pg_tables WHERE schemaname != 'pg_catalog' AND schemaname != 'information_schema';`
