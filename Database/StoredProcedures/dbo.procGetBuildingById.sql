CREATE PROCEDURE [dbo].[procGetBuildingById]
	@Id int
AS
BEGIN
	SELECT [Id], [Name]
	FROM [dbo].[Building]
	WHERE [Id]=@Id
END