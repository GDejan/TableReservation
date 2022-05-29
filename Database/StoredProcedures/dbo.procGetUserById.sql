CREATE PROCEDURE [dbo].[procGetUsersById]
	@Id int
AS
BEGIN
	SELECT [Id], [Name], [Surname], [Username], [IsAdmin]
	FROM [dbo].[Users]
	WHERE [Id]=@Id
END