CREATE PROCEDURE [dbo].[procNewReservation]
	@BuildingId int,
	@StoreyId int,
	@RoomId int,
	@DeskId int,
	@UserId int,
	@ReservedAt datetime
AS
begin
	INSERT INTO [dbo].[Reservations] 
	VALUES (@BuildingId, @StoreyId, @RoomId, @DeskId, @UserId, @ReservedAt, getdate())
END