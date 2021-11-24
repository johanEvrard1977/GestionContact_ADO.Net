CREATE PROCEDURE [dbo].[SP_Add_User]
	@FirstName NVARCHAR(32),
	@LastName NVARCHAR(32),
	@pass NVARCHAR(64),
	@mail NVARCHAR(64)
AS
	INSERT INTO [dbo].[User]([FirstName], [LastName], [Email], [Password]) VALUES
	(@FirstName,@LastName,@mail,dbo.SF_HashPassword(@pass))
