CREATE PROCEDURE [dbo].[procRemoveBuilding]
	@Id int
AS
begin
	DELETE [dbo].[Building]
	WHERE [Id]=@Id
END