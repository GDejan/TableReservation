CREATE PROCEDURE [dbo].[procGetResForDate]
@BuildingId int,
@StoreyId int,
@RoomId int,
@DeskId int,
@ReservedAt datetime

AS
BEGIN
	SELECT [Id], [BuildingId], [StoreyId], [RoomId], [DeskId], [UserId], [ReservedAt], [TimeStamp]
	FROM [dbo].[Reservations]
	WHERE [BuildingId]=@BuildingId and [StoreyId]=@StoreyId and [RoomId]=@RoomId and [DeskId]=@DeskId and [ReservedAt]=@ReservedAt
END