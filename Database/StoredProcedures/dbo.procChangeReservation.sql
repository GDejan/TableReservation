CREATE PROCEDURE [dbo].[procChangeReservation]
	@BuildingId int,
	@StoreyId int,
	@RoomId int,
	@DeskId int,
	@UserId int,
	@ReservedAt datetime
AS
begin
	UPDATE [dbo].[Reservations] 
	SET [BuildingId]=@BuildingId, [StoreyId]=@StoreyId, [RoomId]=@RoomId, [DeskId]=@DeskId, [UserId]=@UserId, [ReservedAt]=@ReservedAt, getdate()
	WHERE [Id]=@Id
END