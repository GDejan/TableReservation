CREATE PROCEDURE [dbo].[procChangeBuilding]
	@Id int,
	@name nvarchar(50)
AS
begin
	UPDATE [dbo].[Building]
	SET [Name]=@Name
	WHERE [Id]=@Id
END