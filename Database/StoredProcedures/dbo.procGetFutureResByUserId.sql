CREATE PROCEDURE [dbo].[procGetFutureResByUserId]
@UserId int,
@ReservedAtStart datetime


AS
BEGIN
	SELECT Reservations.Id as Id, Building.Name as BuildingName, Storey.Name as StoreyName, Room.Name as RoomName, Desk.Name as DeskName,  CONCAT(Building.Name, '-', Storey.Name,  Room.Name, '-', Desk.Name) AS FullName, ReservedAt as ReservedAt
	FROM ((((Reservations
	INNER JOIN Building ON Reservations.BuildingId=Building.Id)
	INNER JOIN Storey ON Reservations.StoreyId=Storey.Id)
	INNER JOIN Room ON Reservations.RoomId=Room.Id)
	INNER JOIN Desk ON Reservations.DeskId=Desk.Id)
	WHERE [UserId]=@UserId and [ReservedAt]>=@ReservedAtStart
END