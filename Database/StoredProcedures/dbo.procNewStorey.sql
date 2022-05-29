CREATE PROCEDURE [dbo].[procNewStorey]
	@Name nvarchar(50)
AS
begin
	INSERT INTO [dbo].[Storey]
	VALUES (@Name)
END