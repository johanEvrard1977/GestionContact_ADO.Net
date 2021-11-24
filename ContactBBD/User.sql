CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL IDENTITY,
	[FirstName] VARCHAR(50) NOT NULL,
	[LastName] VARCHAR(50) NOT NULL,
	[Email] VARCHAR(50) NOT NULL,
	[Password] VARBINARY(32) NOT NULL,
	constraint PK_UserId PRIMARY KEY ([Id]),
	constraint UK_USEREMAIL UNIQUE ([Email])
)
