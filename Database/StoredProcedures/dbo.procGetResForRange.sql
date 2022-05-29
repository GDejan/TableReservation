CREATE PROCEDURE [dbo].[procGetResForRange]
@ReservedAtStart Datetime,
@ReservedAtEnd datetime
AS
BEGIN
	SELECT [Id], [BuildingId], [StoreyId], [RoomId], [DeskId], [UserId], [ReservedAt],[TimeStamp]
	FROM [dbo].[Reservations]
	WHERE [ReservedAt] BETWEEN @ReservedAtStart and @ReservedAtEnd
END