CREATE PROCEDURE [dbo].[dbo.procGetResById]
	@Id int
AS
BEGIN
	SELECT [Id], [BuildingId], [StoreyId], [RoomId], [DeskId], [UserId], [ReservedAt], [TimeStamp]
	FROM [dbo].[Reservations]
	WHERE [Id]=@Id
END