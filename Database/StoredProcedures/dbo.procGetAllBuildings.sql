CREATE PROCEDURE [dbo].[procGetAllBuildings]
AS
BEGIN
	SELECT [Id], [Name]
	FROM [dbo].[Building]
END