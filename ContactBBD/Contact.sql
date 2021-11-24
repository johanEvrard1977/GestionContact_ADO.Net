CREATE TABLE [dbo].[Contact]
(
	[Id] INT NOT NULL identity,
Nom nvarchar(75) not null,
Prenom nvarchar(75) not null,
Email nvarchar(384) not null, 
Tel nvarchar(20) not null,
Date_de_naissance Date not null,
UserId int not null,
Constraint PK_Contact PRIMARY KEY (Id),
Constraint UK_Contact_Email UNIQUE (Email),
constraint FK_ContactUserid FOREIGN KEY (UserId) REFERENCES [User](Id)
)
