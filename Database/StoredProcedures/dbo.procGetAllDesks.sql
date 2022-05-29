CREATE PROCEDURE [dbo].[procGetAllDesks]
AS
BEGIN
	SELECT [Id], [Name]
	FROM [dbo].[Desk]
END