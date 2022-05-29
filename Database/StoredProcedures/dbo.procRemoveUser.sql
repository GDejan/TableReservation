CREATE PROCEDURE [dbo].[procRemoveUser]
	@Id int
AS
BEGIN
	DELETE [dbo].[Users]
	WHERE [Id]=@Id
END