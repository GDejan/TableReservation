CREATE PROCEDURE [dbo].[procNewUser]
	@FirstName nvarchar(50),
	@SurName nvarchar(50),
	@UserName nvarchar(50),
	@PassWord nvarchar(100),
	@IsAdmin bit	,
	@IsTemp bit	
AS
begin
	INSERT INTO [dbo].[Users] 
	VALUES (@FirstName, @SurName, @UserName, @PassWord, @IsAdmin, @IsTemp)
END