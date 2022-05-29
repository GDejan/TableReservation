CREATE TABLE [dbo].[Reservations] (
    [Id]         INT      IDENTITY (1, 1) NOT NULL,
    [BuildingId] INT      NOT NULL,
    [StoreyId]   INT      NOT NULL,
    [RoomId]     INT      NOT NULL,
    [DeskId]     INT      NOT NULL,
    [UserId]     INT      NOT NULL,
    [ReservedAt] DATETIME NOT NULL,
    [TimeStamp]  DATETIME NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Reservations_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]),
    FOREIGN KEY ([BuildingId]) REFERENCES [dbo].[Building] ([Id]),
    FOREIGN KEY ([DeskId]) REFERENCES [dbo].[Desk] ([Id]),
    FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Room] ([Id]),
    FOREIGN KEY ([StoreyId]) REFERENCES [dbo].[Storey] ([Id])
);

