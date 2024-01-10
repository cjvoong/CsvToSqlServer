-- schema.sql

-- Switch to the master database to create the new database
USE master;
GO

-- Create your database
CREATE DATABASE CsvToSqlServerDatabase;
GO

-- Switch to the newly created database
USE CsvToSqlServerDatabase;
GO

-- Create your schema and tables
CREATE SCHEMA CsvToSqlServer;
GO

CREATE TABLE CsvToSqlServer.Customer (
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,
    Forename NVARCHAR(255) NOT NULL,
    Surname NVARCHAR(255) NOT NULL,
    Postcode NVARCHAR(20) NOT NULL
);
GO

-- Create a SQL Server login
CREATE LOGIN DataLoadUser WITH PASSWORD = '3qoLxoCRw7';
GO

-- Switch back to the database to create the user and grant permissions
USE CsvToSqlServerDatabase;
GO

-- Create a user for the login
CREATE USER DataLoadUser FOR LOGIN DataLoadUser;
GO

-- Grant read (SELECT) and write (INSERT, UPDATE, DELETE) privileges to the user
GRANT SELECT, INSERT, UPDATE, DELETE ON OBJECT::CsvToSqlServer.Customer TO DataLoadUser;
GO
