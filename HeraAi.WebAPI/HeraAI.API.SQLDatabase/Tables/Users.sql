﻿CREATE TABLE [dbo].[Users]
(

	[Id] NVARCHAR(6) NOT NULL,
	[CountryId] NVARCHAR(6) NULL,
	[Language] NVARCHAR(6) NOT NULL DEFAULT 'en-US',
	[Email] NVARCHAR(500) NOT NULL,
	[Password] NVARCHAR(150) NOT NULL,
	[FirstName] NVARCHAR(150) NOT NULL,
	[LastName] NVARCHAR(150) NOT NULL,
	[Phone] NVARCHAR(20) NULL,

	-- BASE PROPERTIES
	[Inactive] BIT NOT NULL,
	[CreationDate] DATETIME NOT NULL,
	[LastUpdate] DATETIME NOT NULL,
	[LastUserId] NVARCHAR(6) NOT NULL,

	-- PRIMARY KEY
	CONSTRAINT PK_Users PRIMARY KEY ([Id]),

	-- FOREIGN KEYS
	-- CONSTRAINT FK_Users_Countries FOREIGN KEY (CountryId) REFERENCES [dbo].[Countries]([Id])

);