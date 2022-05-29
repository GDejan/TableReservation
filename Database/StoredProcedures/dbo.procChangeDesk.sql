CREATE PROCEDURE [dbo].[procChangeDesk]
	@Id int,
	@name nvarchar(50)
AS
begin
	UPDATE [dbo].[Desk]
	SET [Name]=@Name
	WHERE [Id]=@Id
END