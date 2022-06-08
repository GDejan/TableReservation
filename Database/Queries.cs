namespace TableReservation.Database
{
    public class Queries
    {
        public string procNewUser = "INSERT INTO [Users] VALUES (null, @Name, @Surname, @Username, @Password, @IsAdmin, @IsTemp)";
        public string procChangeUser = "UPDATE [Users] SET [Name] = @Name, [Surname] = @Surname, [Username] = @Username, [IsAdmin] = @IsAdmin, [IsTemp] = @IsTemp WHERE [Id] = @Id";
        public string procChangeUserPass = "UPDATE [Users] SET [Password] = @Password, [IsTemp]=@IsTemp WHERE [Id] = @Id";
        public string procRemoveUser = "DELETE FROM [Users] WHERE [Id] = @Id";
        public string procGetUserLoginCheck = "SELECT [Id], [Name], [Surname], [Username], [Password], [IsAdmin], [IsTemp] FROM [Users] WHERE [Username] = @Username AND [Password] = @Password";
        public string procGetUsersById = "SELECT [Id], [Name], [Surname], [Username], [IsAdmin], [IsTemp] FROM [Users] WHERE [Id] = @Id";
        public string procGetUsersByUsername = "SELECT [Id], [Name], [Surname], [Username], [IsAdmin], [IsTemp] FROM [Users] WHERE [Username] = @Username";
        public string procGetAllUsers = "SELECT [Id], [Name], [Surname], [Username], [Password], [IsAdmin], [IsTemp] FROM [Users]";

        public string procNewBuilding = "INSERT INTO [Buildings] VALUES (null,@Name)";
        public string procChangeBuilding = "UPDATE [Buildings] SET [Name] = @Name WHERE [Id] = @Id";
        public string procRemoveBuilding = "DELETE FROM [Buildings] WHERE [Id] = @Id";
        public string procGetBuildingByName = "SELECT [Id], [Name] FROM [Buildings] WHERE [Name] = @Name";
        public string procGetBuildingById = "SELECT [Id], [Name] FROM [Buildings] WHERE [Id] = @Id";
        public string procGetAllBuildings = "SELECT [Id], [Name] FROM [Buildings]";

        public string procNewStorey = "INSERT INTO [Storeys] VALUES (null,@Name)";
        public string procChangeStorey = "UPDATE [Storeys] SET [Name] = @Name WHERE [Id] = @Id";
        public string procRemoveStorey = "DELETE FROM [Storeys] WHERE [Id] = @Id";
        public string procGetStoreyByName = "SELECT [Id], [Name] FROM [Storeys] WHERE [Name] = @Name";
        public string procGetStoreyById = "SELECT [Id], [Name] FROM [Storeys] WHERE [Id] = @Id";
        public string procGetAllStoreys = "SELECT [Id], [Name] FROM [Storeys]";

        public string procNewRoom = "INSERT INTO [Rooms] VALUES (null,@Name)";
        public string procChangeRoom = "UPDATE [Rooms] SET [Name] = @Name WHERE [Id] = @Id";
        public string procRemoveRoom = "DELETE FROM [Rooms] WHERE [Id] = @Id";
        public string procGetRoomByName = "SELECT [Id], [Name] FROM [Rooms] WHERE [Name] = @Name";
        public string procGetRoomById = "SELECT [Id], [Name] FROM [Rooms] WHERE [Id] = @Id";
        public string procGetAllRooms = "SELECT [Id], [Name] FROM [Rooms]";

        public string procNewDesk = "INSERT INTO [Desks] VALUES (null,@Name)";
        public string procChangeDesk = "UPDATE [Desks] SET [Name] = @Name WHERE [Id] = @Id";
        public string procRemoveDesk = "DELETE FROM [Desks] WHERE [Id] = @Id";
        public string procGetDeskByName = "SELECT [Id], [Name] FROM [Desks] WHERE [Name] = @Name";
        public string procGetDeskById = "SELECT [Id], [Name] FROM [Desks] WHERE [Id] = @Id";
        public string procGetAllDesks = "SELECT [Id], [Name] FROM [Desks]";

        public string procNewReservation = "INSERT INTO [Reservations] VALUES (null,@BuildingId, @StoreyId, @RoomId, @DeskId, @UserId, @ReservedAt, datetime())";
        public string procChangeReservation = "UPDATE [Reservations] SET [BuildingId] = @BuildingId, [StoreyId] = @StoreyId, [RoomId] = @RoomId, [DeskId] = @DeskId, [UserId] = @UserId, [ReservedAt] = @ReservedAt, datetime() WHERE [Id] = @Id";
        public string procRemoveReservation = "	DELETE FROM [Reservations] WHERE [Id] = @Id";
        public string procGetResById = "	SELECT [Id], [BuildingId], [StoreyId], [RoomId], [DeskId], [UserId], [ReservedAt], [TimeStamp] FROM [Reservations] WHERE [Id] = @Id";
        public string procGetResByUserIdDate = "	SELECT [Id], [BuildingId], [StoreyId], [RoomId], [DeskId], [UserId], [ReservedAt], [TimeStamp] FROM [Reservations] WHERE [UserId] = @UserId AND [ReservedAt] = @ReservedAt";
        public string procGetResForDate = "SELECT [Id], [BuildingId], [StoreyId], [RoomId], [DeskId], [UserId], [ReservedAt], [TimeStamp] FROM [Reservations] WHERE [BuildingId] = @BuildingId AND [StoreyId] = @StoreyId AND [RoomId] = @RoomId AND [DeskId] = @DeskId AND [ReservedAt] = @ReservedAt";
        public string procGetResForRange = "	SELECT [Id], [BuildingId], [StoreyId], [RoomId], [DeskId], [UserId], [ReservedAt],[TimeStamp] FROM [Reservations] WHERE [ReservedAt] BETWEEN @ReservedAtStart AND @ReservedAtEnd";
        public string procGetFutureResByUserId = "SELECT Reservations.Id as Id, Buildings.Name as BuildingName, Storeys.Name as StoreyName, Rooms.Name as RoomName, Desks.Name as DeskName, Buildings.Name || '-' || Storeys.Name || Rooms.Name || '-' || Desks.Name AS FullName, ReservedAt as ReservedAt FROM ((((Reservations INNER JOIN Buildings ON Reservations.BuildingId = Buildings.Id) INNER JOIN Storeys ON Reservations.StoreyId = Storeys.Id) INNER JOIN Rooms ON Reservations.RoomId = Rooms.Id) INNER JOIN Desks ON Reservations.DeskId = Desks.Id) WHERE [UserId] = @UserId AND [ReservedAt] >= @ReservedAtStart";
        public string procGetResInnerUser = "SELECT [Users].[Name], [Users].Surname FROM Reservations INNER JOIN Users ON Reservations.UserId = Users.Id WHERE [BuildingId] = @BuildingId AND [StoreyId] = @StoreyId AND [RoomId] = @RoomId AND [DeskId] = @DeskId AND [ReservedAt] = @ReservedAt";
        public string procGetResForDateDesk = "SELECT Reservations.Id as Id, [Users].[Name] as Name, [Users].Surname as Surname, ReservedAt as ReservedAt FROM Reservations INNER JOIN Users ON Reservations.UserId = Users.Id WHERE [BuildingId] = @BuildingId AND [StoreyId] = @StoreyId AND [RoomId] = @RoomId AND [DeskId] = @DeskId AND [ReservedAt] >= @ReservedFrom";
        public string procGetAllRes = "SELECT [Id], [BuildingId], [StoreyId], [RoomId], [DeskId], [UserId], [ReservedAt], [TimeStamp] FROM [Reservations]";

    }
}
