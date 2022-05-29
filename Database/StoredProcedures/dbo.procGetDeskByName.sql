CREATE PROCEDURE [dbo].[procGetDeskByName]
	@Name nvarchar(50)
AS
BEGIN
	SELECT [Id], [Name]
	FROM [dbo].[Desk]
	WHERE [Name]=@Name
END