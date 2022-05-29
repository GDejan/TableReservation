CREATE PROCEDURE [dbo].[procGetRoomByName]
	@Name nvarchar(50)
AS
BEGIN
	SELECT [Id], [Name]
	FROM [dbo].[Room]
	WHERE [Name]=@Name
END