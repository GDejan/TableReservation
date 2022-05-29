CREATE PROCEDURE [dbo].[procGetResInnerUser]
@BuildingId int,
@StoreyId int,
@RoomId int,
@DeskId int,
@ReservedAt datetime

AS
BEGIN
	SELECT [Users].[Name], [Users].Surname
	FROM Reservations
	INNER JOIN Users ON Reservations.UserId=Users.Id
	WHERE [BuildingId]=@BuildingId and [StoreyId]=@StoreyId and [RoomId]=@RoomId and [DeskId]=@DeskId and [ReservedAt]=@ReservedAt
END