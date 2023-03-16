USE [master]
GO
IF db_id('SpyDuh') IS NULL
    CREATE DATABASE [SpyDuh]
GO
USE [SpyDuh]
GO

DROP TABLE IF EXISTS [Skill];
DROP TABLE IF EXISTS [Services];
DROP TABLE IF EXISTS [Friends];
DROP TABLE IF EXISTS [Enemies];
DROP TABLE IF EXISTS [Spy];

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
  [skillName] nvarchar(255) NOT NULL,
  [spyId] int NOT NULL,
  [skillLevel] int NOT NULL,
)
GO

CREATE TABLE [Services] (
  [id] integer PRIMARY KEY identity NOT NULL,
  [serviceName] nvarchar(255) NOT NULL,
  [cost] int NOT NULL,
  [spyId] int
)
GO

CREATE TABLE [Friends] (
  [id] integer PRIMARY KEY identity NOT NULL,
  [spyId] int NOT NULL,
  [friendId] int
)
GO

CREATE TABLE [Enemies] (
  [id] integer PRIMARY KEY identity NOT NULL,
  [spyId] int NOT NULL,
  [enemyId] int
)
GO

ALTER TABLE [Skill] ADD FOREIGN KEY ([spyId]) REFERENCES [Spy] ([id])
GO

ALTER TABLE [Friends] ADD FOREIGN KEY ([spyId]) REFERENCES [Spy] ([id])
GO

ALTER TABLE [Enemies] ADD FOREIGN KEY ([spyId]) REFERENCES [Spy] ([id])
GO

ALTER TABLE [Enemies] ADD FOREIGN KEY ([enemyId]) REFERENCES [Spy] ([id])
GO

ALTER TABLE [Friends] ADD FOREIGN KEY ([friendId]) REFERENCES [Spy] ([id])
GO

ALTER TABLE [Services] ADD FOREIGN KEY ([spyId]) REFERENCES [Spy] ([id])
GO

SET IDENTITY_INSERT [Spy] ON
INSERT INTO [Spy]
  ([Id], [name], [userName], [email], [isMember], [DateCreated])
VALUES 
  (1, 'Robert', 'rStroud', 'robert@text.com', 1, '06-02-2019'),
  (2, 'Ethan', 'eSawicki', 'ethan@text.com', 1, '07-21-2020'),
  (3, 'Mitchell', 'mShelton', 'mitchell@text.com', 1, '06-21-2021'),
  (4, 'Erica', 'eClayton', 'erica@text.com', 1, '01-30-2022'),
  (5, 'Thomas', 'tKibby', 'thomas@text.com', 1, '03-30-2021');
SET IDENTITY_INSERT [Spy] OFF

SET IDENTITY_INSERT [Skill] ON
INSERT INTO [Skill]
  ([id], [skillName], [spyId], [skillLevel])
VALUES
  (1, 'Sneaking', 1, 5),
  (2, 'Lockpicking', 1, 5),
  (3, 'Espionage', 1, 4),
  (4, 'Hacking', 2, 5),
  (5, 'Sneaking', 2, 5);

SET IDENTITY_INSERT [Skill] OFF

SET IDENTITY_INSERT [Services] ON
INSERT INTO [Services]
  ([id], [serviceName], [spyId], [cost])
VALUES
  (1, 'Assassination', 1, 100),
  (2, 'Bankrobbery', 1, 5),
  (3, 'smuggling', 1, 4),
  (4, 'seduction', 2, 5),
  (5, 'forgery', 2, 5);

SET IDENTITY_INSERT [Services] OFF

SET IDENTITY_INSERT [Friends] ON
INSERT INTO [Friends]
  ([id], [spyId], [friendId])
VALUES
  (1, 1, 3 ),
  (2, 2, 4),
  (3, 3, 4),
  (4, 4, 2),
  (5, 3, 1);

SET IDENTITY_INSERT [Friends] OFF

SET IDENTITY_INSERT [Enemies] ON
INSERT INTO [Enemies]
  ([id], [spyId], [enemyId])
VALUES
  (1, 1, 2 ),
  (2, 2, 3),
  (3, 3, 2),
  (4, 4, 1),
  (5, 3, 3);

SET IDENTITY_INSERT [Enemies] OFF
