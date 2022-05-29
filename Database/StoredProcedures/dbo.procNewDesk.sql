CREATE PROCEDURE [dbo].[procNewDesk]
	@Name nvarchar(50)
AS
begin
	INSERT INTO [dbo].[Desk]
	VALUES (@Name)
END