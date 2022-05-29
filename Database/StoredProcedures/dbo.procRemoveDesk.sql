CREATE PROCEDURE [dbo].[procRemoveDesk]
	@Id int
AS
begin
	DELETE [dbo].[Desk]
	WHERE [Id]=@Id
END