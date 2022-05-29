CREATE PROCEDURE [dbo].[procChangeStorey]
	@Id int,
	@name nvarchar(50)
AS
begin
	UPDATE [dbo].[Storey]
	SET [Name]=@Name
	WHERE [Id]=@Id
END