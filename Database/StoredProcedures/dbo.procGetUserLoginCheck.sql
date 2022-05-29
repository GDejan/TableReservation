CREATE PROCEDURE [dbo].[procGetUserLoginCheck]
	@Username nvarchar(50),
	@Password nvarchar(100)
AS
BEGIN
	SELECT [Id], [Name], [Surname], [Username], [Password], [IsAdmin], [IsTemp]
	FROM [dbo].[Users]
	WHERE [Username]=@Username AND [Password]=@Password
END