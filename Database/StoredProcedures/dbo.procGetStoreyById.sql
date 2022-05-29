CREATE PROCEDURE [dbo].[procGetStoreyById]
	@Id int
AS
BEGIN
	SELECT [Id], [Name]
	FROM [dbo].[Storey]
	WHERE [Id]=@Id
END