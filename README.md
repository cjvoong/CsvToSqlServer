# CsvToSqlServer

This is a sample tool to show a simple data load from CSV to MSSQL.

## Dockerfile for mssql

Due to the fact that MSSQL is awkward and doesn't run schema updates on start for you automatically, this was a slightly hand cranked example based on stackoverflow answers.

- Use the existing microsoft docker image and install a few useful tools.
- Create an entrypoint script which will try to log into intermittently then run the schema.sql script.
- Had to add another call to sqlserver to make sure the container doesn't shut down (something to look into later).

## Running

- Start the mssql, and preload the schema and db needed
`docker build . -t mssql_preloaded`
`docker run -p 1433:1433 mssql_preloaded`

- Run the dataload against it
`dotnet run`

- Check your data is in the database, you can use something like sqlcmd or similar

## Could dos

- Add a select at the end to verify the data is there
- Write some tests