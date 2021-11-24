CREATE FUNCTION [dbo].[SF_HashPassword]
(
	@password NVARCHAR(64)
)
RETURNS VARBINARY(32)
AS
BEGIN
	DECLARE @passwordWithNoise NVARCHAR(MAX)
	SET @passwordWithNoise = 'Salut'+@password+'les amis!'
	RETURN HASHBYTES('SHA2_256',@passwordWithNoise)
END
