-- init.sql
-- This script runs on database startup

-- Check if the database exists, and create it if it does not
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Weather')
BEGIN
    CREATE DATABASE Weather;
END
GO

-- Use the database
USE Weather;
GO

-- Check if the Forecasts table exists, and create it if it does not
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'WeatherForecasts')
BEGIN
    CREATE TABLE WeatherForecasts (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Date DATETIME NOT NULL,
        TemperatureC INT NOT NULL,
        Summary NVARCHAR(50) NOT NULL
    );
END
GO

--Check if the Forecasts table is empty, else prefill with some data
IF NOT EXISTS (SELECT * FROM WeatherForecasts)
BEGIN
	INSERT INTO WeatherForecasts (Date, TemperatureC, Summary) VALUES
	('2021-01-01', 20, 'Sunny'),
	('2021-01-02', 22, 'Cloudy'),
	('2021-01-03', 18, 'Rainy'),
    ('2021-01-04', 25, 'Sunny'),
	('2021-01-05', 23, 'Cloudy'),
	('2021-01-06', 19, 'Rainy'),
	('2021-01-07', 24, 'Sunny'),
	('2021-01-08', 21, 'Cloudy'),
	('2021-01-09', 17, 'Rainy'),
	('2021-01-10', 26, 'Sunny');
END
GO