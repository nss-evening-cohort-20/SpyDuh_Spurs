USE [master]
GO
IF db_id('SpyDuh') IS NULL
    CREATE DATABASE [SpyDuh]
GO
USE [SpyDuh]
GO

DROP TABLE IF EXISTS [Spy];
DROP TABLE IF EXISTS [Skill];
DROP TABLE IF EXISTS [Services];
DROP TABLE IF EXISTS [Friends];
DROP TABLE IF EXISTS [Enemies];

CREATE TABLE [Spy] (
  [id] integer PRIMARY KEY identity NOT NULL,
  [name] nvarchar(255) NOT NULL,
  [userName] nvarchar(255) NOT NULL,
  [email] nvarchar(255) NOT NULL,
  [isMember] BIT,
  [DateCreated] datetime NOT NULL
)
GO

CREATE TABLE [Skill] (
  [id] integer PRIMARY KEY identity NOT NULL,
  [skill] nvarchar(255) NOT NULL,
  [spyId] int NOT NULL,
  [skillLevel] nvarchar(255)
)
GO

CREATE TABLE [Services] (
  [id] integer PRIMARY KEY identity NOT NULL,
  [service] nvarchar(255) NOT NULL,
  [cost] int NOT NULL,
  [spyId] int
)
GO

CREATE TABLE [Friends] (
  [id] integer PRIMARY KEY identity NOT NULL,
  [spyId] int NOT NULL,
  [friendsId] int
)
GO

CREATE TABLE [Enemies] (
  [id] integer PRIMARY KEY identity NOT NULL,
  [spyId] int NOT NULL,
  [enemiesId] int
)
GO

ALTER TABLE [Skill] ADD FOREIGN KEY ([spyId]) REFERENCES [Spy] ([id])
GO

ALTER TABLE [Friends] ADD FOREIGN KEY ([spyId]) REFERENCES [Spy] ([id])
GO

ALTER TABLE [Enemies] ADD FOREIGN KEY ([spyId]) REFERENCES [Spy] ([id])
GO

ALTER TABLE [Enemies] ADD FOREIGN KEY ([enemiesId]) REFERENCES [Spy] ([id])
GO

ALTER TABLE [Friends] ADD FOREIGN KEY ([friendsId]) REFERENCES [Spy] ([id])
GO

ALTER TABLE [Services] ADD FOREIGN KEY ([spyId]) REFERENCES [Spy] ([id])
GO