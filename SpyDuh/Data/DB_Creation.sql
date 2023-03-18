USE [master]
GO
IF db_id('SpyDuh') IS NULL
    CREATE DATABASE [SpyDuh]
GO
USE [SpyDuh]
GO
DROP TABLE IF EXISTS [SKillJoin];
DROP TABLE IF EXISTS [ServiceJoin];
DROP TABLE IF EXISTS [Skill];
DROP TABLE IF EXISTS [Service];
DROP TABLE IF EXISTS [Services];
DROP TABLE IF EXISTS [Friends];
DROP TABLE IF EXISTS [Enemy];
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
)
GO
CREATE TABLE [SkillJoin](
[id] integer PRIMARY KEY identity NOT NULL,
[skillId] int NOT NULL,
[skillLevel] int,
[spyId] int NOT NULL
)
GO
CREATE TABLE [Service] (
  [id] integer PRIMARY KEY identity NOT NULL,
  [serviceName] nvarchar(255) NOT NULL,
)
GO
CREATE TABLE [ServiceJoin](
	[id] integer PRIMARY KEY identity NOT NULL,
	[serviceId] int NOT NULL,
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
CREATE TABLE [Enemy] (
  [id] integer PRIMARY KEY identity NOT NULL,
  [spyId] int NOT NULL,
  [enemySpyId] int
)
GO
ALTER TABLE [Skilljoin] ADD FOREIGN KEY ([skillId]) REFERENCES [Skill] ([id])
GO
ALTER TABLE [Skilljoin] ADD FOREIGN KEY ([spyId]) REFERENCES [Spy] ([id])
GO
ALTER TABLE [Friends] ADD FOREIGN KEY ([spyId]) REFERENCES [Spy] ([id])
GO
ALTER TABLE [Enemy] ADD FOREIGN KEY ([spyId]) REFERENCES [Spy] ([id])
GO
ALTER TABLE [Enemy] ADD FOREIGN KEY ([enemySpyId]) REFERENCES [Spy] ([id])
GO
ALTER TABLE [Friends] ADD FOREIGN KEY ([friendId]) REFERENCES [Spy] ([id])
GO
ALTER TABLE [Servicejoin] ADD FOREIGN KEY ([serviceId]) REFERENCES [Service] ([id])
GO
ALTER TABLE [Servicejoin] ADD FOREIGN KEY ([spyId]) REFERENCES [Spy] ([id])
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
  ([id], [skillName])
VALUES
  (1, 'Sneaking'),
  (2, 'Lockpicking'),
  (3, 'Espionage'),
  (4, 'Hacking'),
  (5, 'Sharpshooting');
SET IDENTITY_INSERT [Skill] OFF
SET IDENTITY_INSERT [SkillJoin] ON
INSERT INTO [SkillJoin]
  ([id], [skillId], [spyId], [skillLevel])
VALUES
  (1, 1, 2, 100),
  (2, 4, 2, 50),
  (3, 2, 3, 75),
  (4, 5, 1, 10),
  (5, 3, 3, 80);
SET IDENTITY_INSERT [SkillJoin] OFF
SET IDENTITY_INSERT [Service] ON
INSERT INTO [Service]
  ([id], [serviceName])
VALUES
  (1, 'Assassination'),
  (2, 'Bankrobbery'),
  (3, 'smuggling'),
  (4, 'seduction'),
  (5, 'forgery');
SET IDENTITY_INSERT [Service] OFF
SET IDENTITY_INSERT [ServiceJoin] ON
INSERT INTO [ServiceJoin]
  ([id], [serviceId], [cost], [spyId])
VALUES
  (1, 2, 500.00, 3),
  (2, 2, 1000.00, 4),
  (3, 3, 250.00, 2),
  (4, 1, 50.00, 1),
  (5, 5, 125.00, 4);
SET IDENTITY_INSERT [ServiceJoin] OFF
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
SET IDENTITY_INSERT [Enemy] ON
INSERT INTO [Enemy]
  ([id], [spyId], [enemySpyId])
VALUES
  (1, 1, 2 ),
  (2, 2, 3),
  (3, 3, 2),
  (4, 4, 1),
  (5, 3, 3);
SET IDENTITY_INSERT [Enemy] OFF