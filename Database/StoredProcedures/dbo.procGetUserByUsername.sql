CREATE PROCEDURE [dbo].[procGetUsersByUsername]
	@Username nvarchar(50)
AS
BEGIN
	SELECT [Id], [Name], [Surname], [Username], [IsAdmin], [IsTemp]
	FROM [dbo].[Users]
	WHERE [Username]=@Username
END