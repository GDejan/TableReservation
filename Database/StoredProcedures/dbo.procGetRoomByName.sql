CREATE PROCEDURE [dbo].[procGetStoreyByName]
	@Name nvarchar(50)
AS
BEGIN
	SELECT [Id], [Name]
	FROM [dbo].[Storey]
	WHERE [Name]=@Name
END