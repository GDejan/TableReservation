CREATE PROCEDURE [dbo].[procChangeUserPass]
	@Id int,
	@Password nvarchar(100) 
AS
BEGIN
	UPDATE [dbo].[Users]
	SET [Password]=@Password
	WHERE [Id]=@Id
END