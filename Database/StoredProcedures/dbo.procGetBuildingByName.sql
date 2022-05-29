CREATE PROCEDURE [dbo].[procGetBuildingByName]
	@Name nvarchar(50)
AS
BEGIN
	SELECT [Id], [Name]
	FROM [dbo].[Building]
	WHERE [Name]=@Name
END