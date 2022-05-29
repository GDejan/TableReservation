CREATE PROCEDURE [dbo].[procRemoveStorey]
	@Id int
AS
begin
	DELETE [dbo].[Storey]
	WHERE [Id]=@Id
END