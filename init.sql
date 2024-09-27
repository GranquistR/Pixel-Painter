
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
    ID int IDENTITY(1,1) NOT NULL,
    ArtistName varchar(20),
    Token varchar(max),
    IsAdmin bit DEFAULT 0, -- this is a bool, 0 = false 1 = true
    CreationDate DATETIME DEFAULT GETDATE(),
    CONSTRAINT PK_Artist PRIMARY KEY (ID),
);
END
GO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Art')
BEGIN
CREATE TABLE Art (
    ID int IDENTITY(1,1) NOT NULL,
    ArtName varchar(255),
	Artistid int,
    Width int,
    ArtLength int,
    Encode varchar(max),
    CreationDate DATETIME DEFAULT GETDATE(),
    isPublic bit DEFAULT 0,
    CONSTRAINT PK_Art PRIMARY KEY (ID),
    CONSTRAINT FK_Art FOREIGN KEY (ArtistID) REFERENCES Artist(ID),
);
END
GO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Likes')
BEGIN
CREATE TABLE Likes (
    ID int IDENTITY(1,1) NOT NULL,
    ArtistID int,
    ArtID int,
    CONSTRAINT PK_Like PRIMARY KEY (ID),
    CONSTRAINT FK_ArtistLike FOREIGN KEY (ArtistID) REFERENCES Artist(ID),
    CONSTRAINT FK_ArtLike FOREIGN KEY (ArtID) REFERENCES Art(ID)
    
);
END
GO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Comment')
BEGIN
CREATE TABLE Comment (
    ID int IDENTITY(1,1) NOT NULL,
    ArtistID int,
    ArtID int,
    Comment varchar(2222),
    CommentTime DATETIME,
    CONSTRAINT PK_Comment PRIMARY KEY (ID),
    CONSTRAINT FK_ArtistComment FOREIGN KEY (ArtistID) REFERENCES Artist(ID),
    CONSTRAINT FK_ArtComment FOREIGN KEY (ArtID) REFERENCES Art(ID)
    
);
END
GO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ArtTags')
BEGIN
CREATE TABLE ArtTags (
    ID int IDENTITY(1,1) NOT NULL,
    TagID int,
    ArtID int,
    CONSTRAINT PK_ArtTags PRIMARY KEY (ID),
    CONSTRAINT FK_TagID FOREIGN KEY (TagID) REFERENCES Tags(ID),
    CONSTRAINT FK_ArtTags FOREIGN KEY (ArtID) REFERENCES Art(ID)
);
END
GO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Tags')
BEGIN
CREATE TABLE Tags (
    ID int IDENTITY(1,1) NOT NULL,
    Tag varchar(255),
    CONSTRAINT PK_Tags PRIMARY KEY (ID)
    
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
--Check if the Forecasts table is empty, else prefill with some data
IF NOT EXISTS (SELECT * FROM Artist)
BEGIN
	INSERT INTO Artist (ArtistName, Token) VALUES
    ('Person1', 'This Is a Token'),
    ('Person2', 'This Is also A token')
END
GO
--Check if the Forecasts table is empty, else prefill with some data
IF NOT EXISTS (SELECT * FROM Art)
BEGIN
	INSERT INTO Art (ArtName,Artistid,ArtLength,Width,Encode) VALUES
    ('Fate Stay Night',1, 20,20,'https://mediaproxy.tvtropes.org/width/1200/https://static.tvtropes.org/pmwiki/pub/images/fate_stay_night_9.png'),
    ('Clannad',2, 20,20,'https://upload.wikimedia.org/wikipedia/en/c/cb/Clannad_game_cover.jpg')
END
GO
--Check if the Forecasts table is empty, else prefill with some data
IF NOT EXISTS (SELECT * FROM Likes)
BEGIN
	INSERT INTO Likes(ArtID,ArtistID) VALUES
    (1,2),
	(2,1)
END
GO



