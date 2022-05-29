CREATE PROCEDURE [dbo].[procGetAllStoreys]
AS
BEGIN
	SELECT [Id], [Name]
	FROM [dbo].[Storey]
END