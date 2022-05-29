CREATE PROCEDURE [dbo].[procNewBuilding]
	@Name nvarchar(50)
AS
begin
	INSERT INTO [dbo].[Building]
	VALUES (@Name)
END