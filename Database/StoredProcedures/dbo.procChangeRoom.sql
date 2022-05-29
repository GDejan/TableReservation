CREATE PROCEDURE [dbo].[procChangeRoom]
	@Id int,
	@name nvarchar(50)
AS
begin
	UPDATE [dbo].[Room]
	SET [Name]=@Name
	WHERE [Id]=@Id
END