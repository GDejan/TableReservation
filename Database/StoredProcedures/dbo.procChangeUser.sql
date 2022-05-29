CREATE PROCEDURE [dbo].[procChangeUser]
	@Id int,
	@Name nvarchar(50), 
	@Surname nvarchar(50), 
	@Username nvarchar(50), 
	@IsAdmin bit,
	@IsTemp bit
AS
BEGIN
	UPDATE [dbo].[Users]
	SET [Name]=@Name, [Surname]=@Surname, [Username]=@Username, [IsAdmin]=@IsAdmin, [IsTemp]=@IsTemp
	WHERE [Id]=@Id
END