CREATE PROCEDURE [dbo].[procGetAllUsers]
AS
BEGIN
	SELECT [Id], [Name], [Surname], [Username], [Password], [IsAdmin], [IsTemp]
	FROM [dbo].[Users]
END