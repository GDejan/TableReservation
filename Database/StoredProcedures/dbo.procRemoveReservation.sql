CREATE PROCEDURE [dbo].[procRemoveReservation]
	@Id int
AS
begin
	DELETE [dbo].[Reservations]
	WHERE [Id]=@Id
END