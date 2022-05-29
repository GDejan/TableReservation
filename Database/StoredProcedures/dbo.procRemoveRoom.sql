CREATE PROCEDURE [dbo].[procRemoveRoom]
	@Id int
AS
begin
	DELETE [dbo].[Room]
	WHERE [Id]=@Id
END