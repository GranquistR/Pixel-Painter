
-- init.sql
-- This script runs on database startup

-- Check if the database exists, and create it if it does not
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'PixelPainter')
BEGIN
CREATE DATABASE PixelPainter;
END
GO

USE PixelPainter;
GO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Artist')
BEGIN
CREATE TABLE Artist (
    ArtistID int NOT NULL,
    ArtistName varchar(20),
    ArtistToken varchar(255),
    IsAdmin bit, -- this is a bool, 0 = false 1 = true
    ArtistCreationDate DATETIME
    CONSTRAINT PK_Artist PRIMARY KEY (ArtistID)
);
END
GO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Art')
BEGIN
CREATE TABLE Art (
    ArtID int NOT NULL,
    ArtName varchar(255),
	Artistid int,
    ArtWidth int,
    ArtLength int,
    ArtEncode varchar(max),
    ArtCreationDate DATETIME,
    isPublic bit,
    CONSTRAINT PK_Art PRIMARY KEY (ArtID),
    CONSTRAINT FK_Art FOREIGN KEY (ArtistID) REFERENCES Artist(ArtistID)
);
END
GO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Likes')
BEGIN
CREATE TABLE Likes (
    LikeID int NOT NULL,
    ArtistID int,
    ArtID int,
    CONSTRAINT PK_Like PRIMARY KEY (LikeID),
    CONSTRAINT FK_ArtistLike FOREIGN KEY (ArtistID) REFERENCES Artist(ArtistID),
    CONSTRAINT FK_ArtLike FOREIGN KEY (ArtID) REFERENCES Art(ArtID)
    
);
END
GO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Comment')
BEGIN
CREATE TABLE Comment (
    CommentID int NOT NULL,
    ArtistID int,
    ArtID int,
    Comment varchar(2222),
    CommentTime DATETIME,
    CONSTRAINT PK_Comment PRIMARY KEY (CommentID),
    CONSTRAINT FK_ArtistComment FOREIGN KEY (ArtistID) REFERENCES Artist(ArtistID),
    CONSTRAINT FK_ArtComment FOREIGN KEY (ArtID) REFERENCES Art(ArtID)
    
);
END
GO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ArtTags')
BEGIN
CREATE TABLE ArtTags (
    ArtTagsID int NOT NULL,
    TagID int,
    ArtID int,
    CONSTRAINT PK_ArtTags PRIMARY KEY (ArtTagsID),
    CONSTRAINT FK_TagID FOREIGN KEY (TagID) REFERENCES Tags(TagID),
    CONSTRAINT FK_ArtTags FOREIGN KEY (ArtID) REFERENCES Art(ArtID)
);
END
GO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Tags')
BEGIN
CREATE TABLE Tags (
    TagID int NOT NULL,
    Tag varchar(255),
    CONSTRAINT PK_Tags PRIMARY KEY (TagID)
    
);
END
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



