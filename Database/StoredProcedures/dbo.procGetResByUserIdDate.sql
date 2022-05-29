CREATE PROCEDURE [dbo].[procGetResByUserIdDate]
@UserId int,
@ReservedAt datetime

AS
BEGIN
	SELECT [Id], [BuildingId], [StoreyId], [RoomId], [DeskId], [UserId], [ReservedAt], [TimeStamp]
	FROM [dbo].[Reservations]
	WHERE [UserId]=@UserId and [ReservedAt]=@ReservedAt
END