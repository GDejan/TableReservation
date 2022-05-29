CREATE PROCEDURE [dbo].[procGetAllRooms]
AS
BEGIN
	SELECT [Id], [Name]
	FROM [dbo].[Room]
END