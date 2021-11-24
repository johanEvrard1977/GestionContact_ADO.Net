CREATE PROCEDURE [dbo].[SP_LoginUser]
    @email NVARCHAR(320), 
    @Passwd NVARCHAR(20)
AS
Begin
    Select Id, FirstName, LastName, Email from [User] where Email = @email and [Password] = dbo.SF_HashPassword(@Passwd);    
End
