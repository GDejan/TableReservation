CREATE PROCEDURE [dbo].[procNewRoom]
	@Name nvarchar(50)
AS
begin
	INSERT INTO [dbo].[Room]
	VALUES (@Name)
END