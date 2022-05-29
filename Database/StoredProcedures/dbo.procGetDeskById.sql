CREATE PROCEDURE [dbo].[procGetDeskById]
	@Id int
AS
BEGIN
	SELECT [Id], [Name]
	FROM [dbo].[Desk]
	WHERE [Id]=@Id
END