CREATE PROCEDURE [dbo].[procGetResForDateDesk]
@BuildingId int,
@StoreyId int,
@RoomId int,
@DeskId int,
@ReservedFrom datetime

AS
BEGIN
	SELECT Reservations.Id as Id, [Users].[Name] as Name, [Users].Surname as Surname, ReservedAt as ReservedAt
	FROM Reservations
	INNER JOIN Users ON Reservations.UserId=Users.Id
	WHERE [BuildingId]=@BuildingId and [StoreyId]=@StoreyId and [RoomId]=@RoomId and [DeskId]=@DeskId and [ReservedAt]>=@ReservedFrom
END