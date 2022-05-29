CREATE PROCEDURE [dbo].[procGetRoomById]
	@Id int
AS
BEGIN
	SELECT [Id], [Name]
	FROM [dbo].[Room]
	WHERE [Id]=@Id
END